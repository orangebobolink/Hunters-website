import {FC} from 'react';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Button, FormControl, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {cn} from '@/shared/lib';
import {format} from 'date-fns';
import {CalendarIcon} from '@radix-ui/react-icons';
import {Calendar} from '@/shared/ui/calendar.tsx';
import {UseFormReturn} from 'react-hook-form';
import {TFunction} from 'i18next';

interface IProps {
    t: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang: string,
    label:string,
    disabled?: (date: Date) =>boolean,
}

const DatePicker : FC<IProps> = ({form, t, name, lang, label, disabled}:IProps) => {
    return (
        <FormField
            control={form.control}
            name={name}
            render={({ field }) => (
                <FormItem className="flex flex-col">
                    <FormLabel>{label}</FormLabel>
                    <Popover>
                        <PopoverTrigger asChild>
                            <FormControl>
                                <Button
                                    variant={"outline"}
                                    className={cn(
                                        "w-full pl-3 text-left font-normal",
                                        !field.value && "text-muted-foreground"
                                    )}
                                >
                                    {field.value ? (
                                        format(field.value, "PPP")
                                    ) : (
                                         <span>Выбери дату</span>
                                     )}
                                    <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                                </Button>
                            </FormControl>
                        </PopoverTrigger>
                        <PopoverContent className="w-auto p-0" align="center">
                            <Calendar
                                mode="single"
                                selected={field.value}
                                onSelect={field.onChange}
                                disabled={disabled}
                                initialFocus
                            />
                        </PopoverContent>
                    </Popover>
                    <FormMessage />
                </FormItem>
            )}
        />
    );
};

export default DatePicker;