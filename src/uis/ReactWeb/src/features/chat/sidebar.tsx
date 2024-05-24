'use client';

import { SquarePen } from 'lucide-react';
import { cn } from '@/shared/lib';
import { Button, buttonVariants } from '@/shared/ui';
import { Link } from 'react-router-dom';
import { Avatar, AvatarImage } from '@/shared/ui/avatar.tsx';
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from '@/shared/ui/tooltip.tsx';
import { Message } from '@/entities/chat/entities/Message';
import { useState } from 'react';
import { UserService } from '@/entities/user/api/UserService';
import SearchBar from '@/entities/chat/ui/search-bar';
import { User } from '@/entities/user/models/User';
import { ChatUser } from '@/entities/user/models/ChatUser';

interface SidebarProps {
    isCollapsed: boolean;
    links: {
        id: string;
        firstName: string;
        lastName: string;
        messages: Message[];
        avatarUrl: string;
        variant: 'grey' | 'ghost';
    }[];
    onClick?: () => void;
    setUsers: (user: ChatUser[]) => void;
    users: ChatUser[];
    setSelectedUser: (user: ChatUser) => void;
}

export function Sidebar({
    links,
    setUsers,
    isCollapsed,
    users,
    setSelectedUser,
}: SidebarProps) {
    const [ordinarUsers, setOrdinarUsers] = useState<User[]>([]);
    const [isOpen, setIsOpen] = useState<boolean>(false);

    const addChat = async () => {
        console.log(users);
        const response = await UserService.getAll();
        const commandUsers = response.data.filter((user) =>
            users.every((u) => u.id !== user.id)
        );
        setOrdinarUsers(commandUsers);
        setIsOpen(true);
    };

    const changeSelectedUser = (id: string) => {
        const user = users.find((user) => user.id === id)!;
        console.log(user);
        setSelectedUser(user);
    };

    return (
        <div
            data-collapsed={isCollapsed}
            className='relative group flex flex-col h-full gap-4 p-2 data-[collapsed=true]:p-2 '
        >
            {!isCollapsed && (
                <div className='flex justify-between p-2 items-center'>
                    <div className='flex gap-2 items-center text-2xl'>
                        <p className='font-medium'>Chats</p>
                        <span className='text-zinc-300'>({links.length})</span>
                    </div>

                    <div>
                        <Button variant='ghost' onClick={addChat}>
                            <SquarePen size={20} />
                        </Button>
                    </div>
                </div>
            )}
            {isOpen && (
                <SearchBar
                    users={users}
                    setUsers={setUsers}
                    commands={ordinarUsers}
                    open={isOpen}
                    setOpen={setIsOpen}
                />
            )}
            <nav className='grid gap-1 px-2 group-[[data-collapsed=true]]:justify-center group-[[data-collapsed=true]]:px-2'>
                {links.map((link, index) =>
                    isCollapsed ? (
                        <TooltipProvider key={index}>
                            <Tooltip key={index} delayDuration={0}>
                                <TooltipTrigger asChild>
                                    <Button
                                        key={index}
                                        variant='ghost'
                                        size='icon'
                                        onClick={() => {
                                            changeSelectedUser(link.id);
                                        }}
                                    >
                                        <Avatar className='flex justify-center items-center'>
                                            <AvatarImage
                                                src={link.avatarUrl}
                                                alt={link.avatarUrl}
                                                width={6}
                                                height={6}
                                                className='w-10 h-10 '
                                            />
                                        </Avatar>{' '}
                                        <span className='sr-only'>
                                            {link.firstName +
                                                ' ' +
                                                link.lastName}
                                        </span>
                                    </Button>
                                </TooltipTrigger>
                                <TooltipContent
                                    side='right'
                                    className='flex items-center gap-4'
                                >
                                    {link.firstName + ' ' + link.lastName}
                                </TooltipContent>
                            </Tooltip>
                        </TooltipProvider>
                    ) : (
                        <Button
                            key={index}
                            variant='ghost'
                            size='lg'
                            onClick={() => {
                                changeSelectedUser(link.id);
                            }}
                        >
                            <Avatar className='flex justify-center items-center'>
                                <AvatarImage
                                    src={link.avatarUrl}
                                    alt={link.avatarUrl}
                                    width={6}
                                    height={6}
                                    className='w-10 h-10 '
                                />
                            </Avatar>
                            <div className='flex flex-col max-w-28'>
                                <span>
                                    {link.firstName + ' ' + link.lastName}
                                </span>
                                {link.messages.length > 0 && (
                                    <span className='text-zinc-300 text-xs truncate '>
                                        {
                                            link.messages[
                                                link.messages.length - 1
                                            ].userId
                                        }
                                        :{' '}
                                        {
                                            link.messages[
                                                link.messages.length - 1
                                            ].content
                                        }
                                    </span>
                                )}
                            </div>
                        </Button>
                    )
                )}
            </nav>
        </div>
    );
}
