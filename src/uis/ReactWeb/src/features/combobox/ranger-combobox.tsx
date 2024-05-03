import * as React from "react"
import { Check, ChevronsUpDown } from "lucide-react"
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Button, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList} from '@/shared/ui/command.tsx';
import {cn} from '@/shared/lib';
import {useEffect, useState} from 'react';
import {UserService} from '@/entities/user/UserService.ts';
import {User} from '@/entities/user/User.ts';
import {TFunction} from 'i18next';
import {UseFormReturn} from 'react-hook-form';

interface IProps {
    t?: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang?: string,
}

export function RangerCombobox({t, form, name, lang}:IProps) {
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
        <FormField
            control={form.control}
            name={name}
            render={({ field }) => (
                <FormItem className="flex flex-col">
                    <FormLabel>Егеря</FormLabel>
                    <Popover open={open} onOpenChange={setOpen}>
                        <PopoverTrigger asChild>
                            <Button
                                variant="outline"
                                role="combobox"
                                aria-expanded={open}
                                className={cn(
                                    "w-full justify-between",
                                    !field.value && "text-muted-foreground"
                                )}
                            >
                                {field.value
                                 ? UserService.getFullName(rangers.find((ranger) => ranger.id === field.value))
                                 : "Выберите егеря..."}
                                <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                            </Button>
                        </PopoverTrigger>
                        <PopoverContent className="w-full p-0">
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
                                                    form.setValue(name, ranger.id)
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
                    <FormMessage />
                </FormItem>
            )}
        />
    )
}
