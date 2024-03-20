import React, {FC} from 'react';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Button, FormControl} from '@/shared/ui';
import {cn} from '@/shared/lib';
import {format} from 'date-fns';
import {CalendarIcon} from '@radix-ui/react-icons';
import {Calendar} from '@/shared/ui/calendar.tsx';
import {ControllerRenderProps, FieldValues} from 'react-hook-form';

interface IProps
{
    field: ControllerRenderProps<FieldValues, string>
}

const DatePicker : FC<IProps> = ({field}) => {
    return (
        <Popover>
            <PopoverTrigger asChild>
                <FormControl>
                    <Button
                        variant={"outline"}
                        className={cn(
                            "w-[240px] pl-3 text-left font-normal",
                            !field.value && "text-muted-foreground"
                        )}
                    >
                        {field.value ? (
                            format(field.value, "PPP")
                        ) : (
                             <span>Pick a date</span>
                         )}
                        <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                    </Button>
                </FormControl>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0" align="start">
                <Calendar
                    mode="single"
                    selected={field.value}
                    onSelect={field.onChange}
                    disabled={(date) =>
                        date > new Date() || date < new Date("1900-01-01")
                    }
                    initialFocus
                />
            </PopoverContent>
        </Popover>
    );
};

export default DatePicker;