import {z} from 'zod';
import {useTranslation} from 'react-i18next';
import {useCallback} from 'react';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {Button, Form} from '@/shared/ui';
import {Trip} from '@/entities/trip/models/Trip.ts';
import {TripService} from '@/entities/trip/api/TripService.ts';
import DatePicker from '@/features/form/date-picker.tsx';

const formSchema = z.object({
    eventData: z.date(),
});

interface IProps {
    trip: Trip
}

const AddDataEventForm = ({trip} : IProps) => {
    const { t} = useTranslation("translation",
        {
            keyPrefix: "trip.create"
        });

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            trip.eventDate = values.eventData;

            try {
                const data = await TripService.update(trip);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Дата добавленна успешно",
                    })
                }
            }catch {
                toast({
                    variant: "destructive",
                    title: "Что-то пошло не так",
                })
            }

        },
        [],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
    })

    const fromDate:Date = trip.permission?.fromDate!
    const toDate:Date = trip.permission?.toDate!

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                <div className="flex flex-col justify-around space-y-2">
                    <DatePicker form={form} t={t}
                                    label="Дата охоты"
                                    name="eventData"
                                    lang="eventData"
                                    disabled={(date:Date) => date < new Date(fromDate) || date > new Date(toDate)}/>
                </div>
                <Button type="submit" className="w-full">
                    Добавить дату
                </Button>
            </form>
        </Form>
    );
};

export default AddDataEventForm;