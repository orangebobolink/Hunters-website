import { UseFormReturn} from 'react-hook-form';
import {Button, FormControl, FormDescription, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {Command, CommandEmpty, CommandGroup, CommandInput, CommandItem, CommandList} from '@/shared/ui/command.tsx';
import {CaretSortIcon, CheckIcon} from '@radix-ui/react-icons';
import {cn} from '@/shared/lib';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {useEffect, useState} from 'react';
import {Land} from '@/entities/land/Land.ts';
import {LandService} from '@/entities/land/LandService.ts';
import {TFunction} from 'i18next';

interface IProps {
    t?: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang?: string,
}

export function LandCombobox({form, name}:IProps) {
    const [lands, setLands] = useState<Land[]>([])

    useEffect(() => {
        const fetchLands = async () => {
            try {
                const response = await LandService.getAll();
                setLands(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchLands();
    }, []);

    return (
        <FormField
            control={form.control}
            name={name}
            render={({ field }) => (
                <FormItem className="flex flex-col">
                    <FormLabel>Локация</FormLabel>
                    <Popover>
                        <PopoverTrigger asChild>
                            <FormControl>
                                <Button
                                    variant="outline"
                                    role="combobox"
                                    className={cn(
                                        "w-[200px] justify-between",
                                        !field.value && "text-muted-foreground"
                                    )}
                                >
                                    {field.value
                                     ? lands.find(
                                            (land) => land.id === field.value
                                        )?.name
                                     : "Выберите локацию"}
                                    <CaretSortIcon className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                                </Button>
                            </FormControl>
                        </PopoverTrigger>
                        <PopoverContent className="w-[200px] p-0">
                            <Command>
                                <CommandInput
                                    placeholder="Поиск локаций..."
                                    className="h-9"
                                />
                                <CommandEmpty>Локаций не обнареженно</CommandEmpty>
                                <CommandGroup>
                                    <CommandList>
                                        {lands.map((land) => (
                                            <CommandItem
                                                value={land.name}
                                                key={land.id}
                                                onSelect={() => {
                                                    form.setValue(name, land.id)
                                                }}
                                            >
                                                {land.name}
                                                <CheckIcon
                                                    className={cn(
                                                        "ml-auto h-4 w-4",
                                                        land.id === field.value
                                                        ? "opacity-100"
                                                        : "opacity-0"
                                                    )}
                                                />
                                            </CommandItem>
                                        ))}
                                    </CommandList>
                                </CommandGroup>
                            </Command>
                        </PopoverContent>
                    </Popover>
                    <FormDescription>
                        This is the language that will be used in the dashboard.
                    </FormDescription>
                    <FormMessage />
                </FormItem>
            )}
        />
    )
}