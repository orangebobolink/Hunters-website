import * as React from "react"
import { Check, ChevronsUpDown } from "lucide-react"
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Button} from '@/shared/ui';
import {Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList} from '@/shared/ui/command.tsx';
import {cn} from '@/shared/lib';
import {useEffect, useState} from 'react';
import {UserService} from '@/entities/user/UserService.ts';
import {User} from '@/entities/user/User.ts';

interface IProps{
    setSelectedRanger: (ranger:User) => void;
}

export function RangerCombobox({setSelectedRanger}:IProps) {
    const [rangers, setRangers] = useState<User[]>([])
    const [open, setOpen] = React.useState(false)
    const [value, setValue] = React.useState("")

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await UserService.getAllRangers();
                setRangers(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, []);

    return (
        <Popover open={open} onOpenChange={setOpen}>
            <PopoverTrigger asChild>
                <Button
                    variant="outline"
                    role="combobox"
                    aria-expanded={open}
                    className="w-[300px] justify-between"
                >
                    {value
                     ? UserService.getFullName(rangers.find((ranger) => ranger.id === value))
                     : "Выберите егеря..."}
                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                </Button>
            </PopoverTrigger>
            <PopoverContent className="w-[300px] p-0">
                <Command>
                    <CommandInput placeholder="Search framework..." />
                    <CommandList>
                        <CommandEmpty>Такой егерь отсуствует</CommandEmpty>
                        <CommandGroup>
                            {rangers.map((ranger) => (
                                <CommandItem
                                    key={ranger.id}
                                    value={ranger.id}
                                    onSelect={(currentValue) => {
                                        setValue(currentValue === value ? "" : currentValue)
                                        setOpen(false)
                                        setSelectedRanger(ranger)
                                    }}
                                >
                                    <Check
                                        className={cn(
                                            "mr-2 h-4 w-4",
                                            value === ranger.id ? "opacity-100" : "opacity-0"
                                        )}
                                    />
                                    {UserService.getFullName(ranger)}
                                </CommandItem>
                            ))}

                        </CommandGroup>
                    </CommandList>
                </Command>
            </PopoverContent>
        </Popover>
    )
}
