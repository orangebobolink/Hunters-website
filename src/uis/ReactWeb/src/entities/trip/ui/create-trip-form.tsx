import {z} from 'zod';
import {useTranslation} from 'react-i18next';
import {useCallback} from 'react';
import {Status} from '@/entities/status/Status.ts';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import InputFormField from '@/features/form/input-form-field.tsx';
import {Button, Form} from '@/shared/ui';
import {Trip} from '@/entities/trip/Trip.ts';
import {TripService} from '@/entities/trip/TripService.ts';
import PermissionCombobox from '@/features/combobox/permission-combobox.tsx';

const formSchema = z.object({
    number: z.string(),
    permissionId: z.string(),
    specialConditions: z.string(),
    price: z.number().int().gte(0)
});

const CreateTripForm = () => {
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.create"
        });

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            const status = Status.Given.toString();

            const request:Trip = {
                number: values.number,
                permissionId: values.permissionId,
                price: values.price,
                specialConditions: values.specialConditions,
                status: status
            }

            try {
                const data = await TripService.create(request);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Путевка созданна успешно",
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

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                <div className="flex flex-col justify-around space-y-2">
                    <InputFormField form={form} t={t}
                                    name="number"
                                    lang="number"/>
                    <PermissionCombobox form={form}
                                        name="permissionId"/>
                    <InputFormField form={form} t={t}
                                    name="specialConditions"
                                    lang="specialConditions"/>
                    <InputFormField form={form} t={t}
                                    name="price"
                                    lang="price"
                                    type="number"/>
                </div>
                <Button type="submit" className="w-full">
                    Добавить
                </Button>
            </form>
        </Form>
    );
};

export default CreateTripForm;