import { useEffect, useState } from 'react';
import { cn } from '@/shared/lib/utils/cnUtil.ts';
import {
    ResizableHandle,
    ResizablePanel,
    ResizablePanelGroup,
} from '@/shared/ui/resizable.tsx';
import { Sidebar } from '@/features/chat/sidebar.tsx';
import { Chat } from '@/features/chat/chat.tsx';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { SignalRContext } from '@/shared/model/signalrR';
import { ChatUser } from '@/entities/user/models/ChatUser';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks';
import { LoadingSpinner } from '@/shared/ui/loading-spinner';
import { Message } from '@/entities/chat/entities/Message';

interface ChatLayoutProps {
    defaultLayout: number[] | undefined;
    defaultCollapsed?: boolean;
    navCollapsedSize: number;
}

export function ChatLayout({
    defaultLayout = [320, 480],
    defaultCollapsed = false,
    navCollapsedSize,
}: ChatLayoutProps) {
    const { id, error } = useAppSelector(selectAuth);
    const [isCollapsed, setIsCollapsed] = useState(defaultCollapsed);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [selectedUser, setSelectedUser] = useState<ChatUser>({
        firstName: '',
        lastName: '',
        messages: [],
        avatarUrl: '',
    });
    const [users, setUsers] = useState<ChatUser[]>([]);
    const [isMobile, setIsMobile] = useState(false);

    const fetchData = async () => {
        try {
            console.log('start');
            const connection = SignalRContext.connection;

            if (connection) {
                if (connection.state === 'Disconnected') {
                    console.log(
                        'Waiting for SignalR connection to be established...'
                    );
                    await connection.start();
                } else if (
                    connection.state === 'Connecting' ||
                    connection.state === 'Reconnecting'
                ) {
                    console.log(
                        'SignalR connection is in the process of being established...'
                    );
                    await new Promise<void | void>((resolve) => {
                        connection.onclose(() => {
                            resolve();
                        });
                        connection.onreconnected(() => {
                            resolve();
                        });
                    });
                }

                if (connection.state === 'Connected') {
                    const response = await connection.invoke(
                        'ReceiveMessages',
                        id
                    );
                    console.log(response);
                    setSelectedUser(response[0]);
                    setUsers(response);
                    setIsLoading(false);
                } else {
                    throw new Error(
                        'SignalR connection could not be established.'
                    );
                }
            } else {
                throw new Error('SignalR connection is not defined.');
            }
            setIsLoading(false);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        if (id) {
            fetchData();
        }
    }, [id]);

    useEffect(() => {
        console.log('Selected User:', selectedUser);
        console.log('Users:', users);
    }, [selectedUser, users]);

    SignalRContext.useSignalREffect(
        'NewMessage',
        (...args) => {
            const message = args[0] as Message;

            if (id == message.toUserId) {
                if (selectedUser.id === message.userId) {
                    console.log('hi');
                    const newSelectedUser = {
                        ...selectedUser,
                        messages: [...selectedUser.messages, message],
                    };
                    setSelectedUser(newSelectedUser);
                }

                let userExists = false;
                const updatedUsers = users.map((u) => {
                    if (u.id == message.userId) {
                        userExists = true;
                        return {
                            ...u,
                            messages: [...u.messages, message],
                        };
                    }
                    return u;
                });

                if (!userExists) {
                    const newUser: ChatUser = {
                        id: message.toUserId,
                        messages: [message],
                        groupId: message.groupId,
                    };
                    updatedUsers.push(newUser);
                }

                setUsers(updatedUsers);  
            }
        },
        []
    );

    useEffect(() => {
        const checkScreenWidth = () => {
            setIsMobile(window.innerWidth <= 768);
        };

        checkScreenWidth();

        window.addEventListener('resize', checkScreenWidth);

        return () => {
            window.removeEventListener('resize', checkScreenWidth);
        };
    }, []);

    return isLoading ? (
        <LoadingSpinner className='w-1/2' />
    ) : (
        <ResizablePanelGroup
            direction='horizontal'
            onLayout={(sizes: number[]) => {
                document.cookie = `react-resizable-panels:layout=${JSON.stringify(
                    sizes
                )}`;
            }}
            className='h-full items-stretch'
        >
            <ResizablePanel
                defaultSize={defaultLayout[0]}
                collapsedSize={navCollapsedSize}
                collapsible={true}
                minSize={isMobile ? 0 : 24}
                maxSize={isMobile ? 8 : 30}
                onCollapse={() => {
                    setIsCollapsed(true);
                    document.cookie = `react-resizable-panels:collapsed=${JSON.stringify(
                        true
                    )}`;
                }}
                onExpand={() => {
                    setIsCollapsed(false);
                    document.cookie = `react-resizable-panels:collapsed=${JSON.stringify(
                        false
                    )}`;
                }}
                className={cn(
                    isCollapsed &&
                        'min-w-[50px] md:min-w-[70px] transition-all duration-300 ease-in-out'
                )}
            >
                <Sidebar
                    isCollapsed={isCollapsed || isMobile}
                    setUsers={setUsers}
                    users={users}
                    links={users.map((user) => ({
                        id: user.id!,
                        firstName: user.firstName,
                        lastName: user.lastName,
                        messages: user.messages ?? [],
                        avatarUrl: user.avatarUrl,
                        variant:
                            selectedUser?.firstName === user.firstName
                                ? 'grey'
                                : 'ghost',
                    }))}
                    setSelectedUser={setSelectedUser}
                />
            </ResizablePanel>
            <ResizableHandle withHandle />
            <ResizablePanel defaultSize={defaultLayout[1]} minSize={30}>
                <Chat selectedUser={selectedUser!} isMobile={isMobile} />
            </ResizablePanel>
        </ResizablePanelGroup>
    );
}
