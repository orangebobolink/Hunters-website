import { useCallback, useEffect, useRef } from 'react';
import ChatBottombar from './chat-bottombar';
import { AnimatePresence, motion } from 'framer-motion';
import { Avatar, AvatarImage } from '@/shared/ui/avatar.tsx';
import { cn } from '@/shared/lib';
import { ScrollArea } from '@/shared/ui/scroll-area.tsx';
import { ChatUser } from '@/entities/user/models/ChatUser';
import { Message } from '../entities/Message';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks';

interface ChatListProps {
    messages?: Message[];
    selectedUser: ChatUser;
    sendMessage: (newMessage: Message) => void;
    isMobile: boolean;
}

export function ChatList({
    messages,
    selectedUser,
    sendMessage,
    isMobile,
}: ChatListProps) {
    const messagesContainerRef = useRef<HTMLDivElement>(null);
    const { avatarUrl } = useAppSelector(selectAuth);

    const scrollToBottom = useCallback(() => {
        messagesContainerRef.current?.scrollIntoView(false);
    }, []);

    useEffect(() => {
        scrollToBottom();
    }, [messages]);

    useEffect(() => {
        scrollToBottom();
    }, [messages]);

    return (
        <div className='w-full overflow-y-auto overflow-x-hidden h-screen flex flex-col'>
            <ScrollArea className='h-screen'>
                <div
                    ref={messagesContainerRef}
                    className='w-full overflow-y-auto overflow-x-hidden flex flex-col'
                >
                    <AnimatePresence>
                        {messages?.map((message, index) => (
                            <motion.div
                                key={index}
                                layout
                                initial={{
                                    opacity: 0,
                                    scale: 1,
                                    y: 50,
                                    x: 0,
                                }}
                                animate={{
                                    opacity: 1,
                                    scale: 1,
                                    y: 0,
                                    x: 0,
                                }}
                                exit={{ opacity: 0, scale: 1, y: 1, x: 0 }}
                                transition={{
                                    opacity: { duration: 0.1 },
                                    layout: {
                                        type: 'spring',
                                        bounce: 0.3,
                                        duration:
                                            messages.indexOf(message) * 0.05 +
                                            0.2,
                                    },
                                }}
                                style={{
                                    originX: 0.5,
                                    originY: 0.5,
                                }}
                                className={cn(
                                    'flex flex-col gap-2 p-4 whitespace-pre-wrap',
                                    message.userId !== selectedUser.id
                                        ? 'items-end'
                                        : 'items-start'
                                )}
                            >
                                <div className='flex gap-3 items-center'>
                                    {message.userId === selectedUser.id && (
                                        <Avatar className='flex justify-center items-center'>
                                            <AvatarImage
                                                src={selectedUser.avatarUrl}
                                                alt={selectedUser.avatarUrl}
                                                width={6}
                                                height={6}
                                            />
                                        </Avatar>
                                    )}
                                    <span className=' bg-accent p-3 rounded-md max-w-xs'>
                                        {message.content}
                                    </span>
                                    {message.userId !== selectedUser.id && (
                                        <Avatar className='flex justify-center items-center'>
                                            <AvatarImage
                                                src={avatarUrl}
                                                alt={avatarUrl}
                                                width={6}
                                                height={6}
                                            />
                                        </Avatar>
                                    )}
                                </div>
                            </motion.div>
                        ))}
                    </AnimatePresence>
                </div>
            </ScrollArea>
            <ChatBottombar sendMessage={sendMessage} isMobile={isMobile} />
        </div>
    );
}
