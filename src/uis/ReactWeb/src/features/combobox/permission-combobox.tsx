import React, {useEffect, useState} from 'react';
import {TFunction} from 'i18next';
import {UseFormReturn} from 'react-hook-form';
import {PermissionService} from '@/entities/permision/PermissionService.ts';
import {Permission} from '@/entities/permision/Permision.ts';
import {Button, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {cn} from '@/shared/lib';
import {Check, ChevronsUpDown} from 'lucide-react';
import {Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList} from '@/shared/ui/command.tsx';

interface IProps {
    t?: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang?: string,
}

const PermissionCombobox = (({t, form, name, lang}:IProps)  => {
    const [permissions, setPermissions] = useState<Permission[]>([])
    const [open, setOpen] = React.useState(false)
    const [value, setValue] = React.useState("")

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await PermissionService.getAll();
                setPermissions(response.data);
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
                    <FormLabel>Разрешения</FormLabel>
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
                                 ? permissions?.find((permission) => permission.id! === field.value)?.number
                                 : "Выберите номер разрешения..."}
                                <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                            </Button>
                        </PopoverTrigger>
                        <PopoverContent className="w-full p-0">
                            <Command>
                                <CommandInput placeholder="Поиск разрешения..." />
                                <CommandList>
                                    <CommandEmpty>Такое разрешение отсуствует</CommandEmpty>
                                    <CommandGroup>
                                        {permissions.map((permission) => (
                                            <CommandItem
                                                key={permission.id}
                                                value={permission.id}
                                                onSelect={(currentValue) => {
                                                    setValue(currentValue === value ? "" : currentValue)
                                                    setOpen(false)
                                                    form.setValue(name, permission.id)
                                                }}
                                            >
                                                <Check
                                                    className={cn(
                                                        "mr-2 h-4 w-4",
                                                        value === permission.id ? "opacity-100" : "opacity-0"
                                                    )}
                                                />
                                                {permission.number}
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
    );
})

export default PermissionCombobox;