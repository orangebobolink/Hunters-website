import {FC} from 'react';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Button, FormControl, FormField, FormItem, FormLabel, FormMessage, Input} from '@/shared/ui';
import {cn} from '@/shared/lib';
import {CalendarIcon} from '@radix-ui/react-icons';
import {Calendar} from '@/shared/ui/calendar.tsx';
import {UseFormReturn} from 'react-hook-form';
import {TFunction} from 'i18next';
import {format} from 'date-fns';

interface IProps {
    t: TFunction,
    form:UseFormReturn<any>,
    name: string,
    lang: string,
    label:string,
    disabled?: (date: Date) =>boolean,
    time?:boolean
}

const DatePicker : FC<IProps> = ({form, t, name, lang, label, disabled, time = false}:IProps) => {

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
                                    variant="outline"
                                    className={cn(
                                        'w-full pl-3 text-left font-normal',
                                        !field.value && 'text-muted-foreground',
                                    )}
                                >
                                    {field.value ? (
                                        time ? `${format(field.value, "dd.MM.yyyy HH:mm")}`
                                              :`${format(field.value, "dd.MM.yyyy")}`
                                    ) : (
                                         <span>{t(lang)}</span>
                                     )}
                                    <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                                </Button>
                            </FormControl>
                        </PopoverTrigger>
                        <PopoverContent className="w-auto p-0" align="center">
                            <Calendar
                                className="p-0"
                                mode="single"
                                selected={field.value}
                                onSelect={field.onChange}
                                disabled={disabled}
                                initialFocus
                            />
                            {
                                time &&
                                <Input
                                    type="time"
                                    className="mt-2"
                                    value={field.value?.toLocaleTimeString([], {
                                        hourCycle: 'h23',
                                        hour: '2-digit',
                                        minute: '2-digit',
                                    })}
                                    onChange={(selectedTime:any) => {
                                        const currentTime = field.value;
                                        currentTime.setHours(
                                            parseInt(selectedTime.target.value.split(':')[0]),
                                            parseInt(selectedTime.target.value.split(':')[1]),
                                            0,
                                        );
                                        field.onChange(currentTime);
                                    }}
                                />
                            }
                        </PopoverContent>
                    </Popover>
                    <FormMessage />
                </FormItem>
            )}
        />
    );
};

export default DatePicker;