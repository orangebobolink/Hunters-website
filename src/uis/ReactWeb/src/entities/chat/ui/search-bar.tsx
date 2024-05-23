import { UserService } from '@/entities/user/api/UserService';
import { ChatUser } from '@/entities/user/models/ChatUser';
import { User } from '@/entities/user/models/User';
import { v4 as uuidv4 } from 'uuid';
import {
    Command,
    CommandEmpty,
    CommandInput,
    CommandItem,
    CommandList,
} from '@/shared/ui/command';
import { useState } from 'react';

interface ICommandProps {
    commands: User[];
    open: boolean;
    setOpen: (item: boolean) => void;
    setUsers: (users: ChatUser[]) => void;
    users: ChatUser[];
}

export default function SearchBar({
    setUsers,
    commands,
    open,
    users,
    setOpen,
}: ICommandProps) {
    const [inputValue, setInputValue] = useState('');

    const filteredCommands = Array.isArray(commands)
        ? commands.filter((command) =>
              UserService.getFullName(command)
                  .toLowerCase()
                  .includes(inputValue.toLowerCase())
          )
        : [];

    const onClick = (user: User) => {
        const newUserChar: ChatUser = {
            id: user.id,
            firstName: user.firstName,
            lastName: user.lastName,
            avatarUrl: user.avatarUrl,
            messages: [],
            groupId: uuidv4(),
        };
        setUsers([...users, newUserChar]);
        setOpen(false);
    };

    return (
        <Command className='rounded-lg border shadow-md z-10'>
            <CommandInput placeholder='Введите ФИО для поиска...' />
            <CommandList>
                <CommandEmpty>Нет такого пользователя.</CommandEmpty>
                {open &&
                    filteredCommands.length > 0 &&
                    filteredCommands.map((command) => (
                        <CommandItem
                            onSelect={(value) => onClick(command)}
                            key={command.id}
                            value={UserService.getFullName(command)}
                        >
                            {UserService.getFullName(command)}
                        </CommandItem>
                    ))}
            </CommandList>
        </Command>
    );
}
