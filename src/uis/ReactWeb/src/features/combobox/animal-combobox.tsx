import React, {useEffect, useState} from 'react';
import {TFunction} from 'i18next';
import {UseFormReturn} from 'react-hook-form';
import {AnimalService} from '@/entities/animal/api/AnimalService.ts';
import {Animal} from '@/entities/animal/models/Animal.ts';
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

const AnimalCombobox = (({t, form, name, lang}:IProps) => {
    const [animals, setAnimals] = useState<Animal[]>([])
    const [open, setOpen] = React.useState(false)
    const [value, setValue] = React.useState("")

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await AnimalService.getAll();
                setAnimals(response.data);
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
                                 ? animals?.find((animal) => animal.id! === field.value)?.name
                                 : "Выберите животное..."}
                                <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                            </Button>
                        </PopoverTrigger>
                        <PopoverContent className="w-full p-0">
                            <Command>
                                <CommandInput placeholder="Search framework..." />
                                <CommandList>
                                    <CommandEmpty>Такой егерь отсуствует</CommandEmpty>
                                    <CommandGroup>
                                        {animals.map((animal) => (
                                            <CommandItem
                                                key={animal.id}
                                                value={animal.id}
                                                onSelect={(currentValue) => {
                                                    setValue(currentValue === value ? "" : currentValue)
                                                    setOpen(false)
                                                    form.setValue(name, animal.id)
                                                }}
                                            >
                                                <Check
                                                    className={cn(
                                                        "mr-2 h-4 w-4",
                                                        value === animal.id ? "opacity-100" : "opacity-0"
                                                    )}
                                                />
                                                {animal.name}
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

export default AnimalCombobox;