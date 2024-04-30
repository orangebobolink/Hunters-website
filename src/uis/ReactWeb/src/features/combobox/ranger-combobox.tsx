import * as React from "react"
import { Check, ChevronsUpDown } from "lucide-react"
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Button} from '@/shared/ui';
import {Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList} from '@/shared/ui/command.tsx';
import {cn} from '@/shared/lib';
import {useEffect, useState} from 'react';
import {AnimalService} from '@/entities/animal/AnimalService.ts';
import {UserService} from '@/entities/user/UserService.ts';
import {User} from '@/entities/user/User.ts';

export function RangerCombobox() {
    const [rangers, serRangers] = useState<User[]>([])
    const [open, setOpen] = React.useState(false)
    const [value, setValue] = React.useState("")

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await UserService.getAllRangers();
                serRangers(response.data);
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
                    className="w-[200px] justify-between"
                >
                    {value
                     ? rangers.find((ranger) => ranger.id === value)?.firstName
                     : "Select framework..."}
                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                </Button>
            </PopoverTrigger>
            <PopoverContent className="w-[200px] p-0">
                <Command>
                    <CommandInput placeholder="Search framework..." />
                    <CommandList>
                        <CommandEmpty>No framework found.</CommandEmpty>
                        <CommandGroup>


                            {rangers.map((ranger) => (
                                <CommandItem
                                    key={ranger.id}
                                    value={ranger.id}
                                    onSelect={(currentValue) => {
                                        setValue(currentValue === value ? "" : currentValue)
                                        setOpen(false)
                                    }}
                                >
                                    <Check
                                        className={cn(
                                            "mr-2 h-4 w-4",
                                            value === ranger.id ? "opacity-100" : "opacity-0"
                                        )}
                                    />
                                    {ranger.firstName}
                                </CommandItem>
                            ))}

                        </CommandGroup>
                    </CommandList>
                </Command>
            </PopoverContent>
        </Popover>
    )
}
