import { z } from 'zod';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { useTranslation } from 'react-i18next';
import { useCallback } from 'react';
import { Status } from '@/entities/status/Status.ts';
import { Land } from '@/entities/land/Land.ts';
import { toast } from '@/shared/ui/use-toast.ts';
import { Permission } from '@/entities/permision/Permision.ts';
import { PermissionService } from '@/entities/permision/PermissionService.ts';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import InputFormField from '@/features/form/input-form-field.tsx';
import DatePicker from '@/features/form/date-picker.tsx';
import { RangerCombobox } from '@/features/combobox/ranger-combobox.tsx';
import { LandCombobox } from '@/features/combobox/land-combobox.tsx';
import { Button, Form } from '@/shared/ui';
import AnimalCombobox from '@/features/combobox/animal-combobox.tsx';

const formSchema = z.object({
    number: z.string().min(5, 'Номер дожен быть юольше 5 символов'),
    fromDate: z.date(),
    toDate: z.date(),
    receivedId: z.string(),
    landId: z.string(),
    animalId: z.string(),
    couponsNumber: z.number().int().gte(0).lte(25),
});

interface IProps {
    setIsOpen: (flag: boolean) => void;
}

const CreatePermissionForm = ({ setIsOpen }: IProps) => {
    const { id } = useAppSelector(selectAuth);
    const { t } = useTranslation('translation', {
        keyPrefix: 'feeding.create',
    });

    const onSubmit = useCallback(async (values: z.infer<typeof formSchema>) => {
        const status =
            values.receivedId.length == 0
                ? Status.Compiled.toString()
                : Status.Given.toString();

        const request: Permission = {
            number: values.number,
            issuedId: id!,
            fromDate: values.fromDate,
            toDate: values.toDate,
            receivedId: values.receivedId,
            animalId: values.animalId,
            landId: values.landId,
            land: { id: values.landId } as Land,
            numberOfCoupons: values.couponsNumber,
            status: status,
        };

        try {
            const data = await PermissionService.create(request);

            if (data.status >= 200 && data.status <= 300) {
                toast({
                    variant: 'success',
                    title: 'Разрешение созданно успешно',
                });

                setIsOpen(false);
            }
        } catch {
            toast({
                variant: 'destructive',
                title: 'Что-то пошло не так',
            });
        }
    }, []);

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
    });

    return (
        <Form {...form}>
            <form
                onSubmit={form.handleSubmit(onSubmit)}
                className='space-y-8 w-full'
            >
                <div className='flex flex-col justify-around space-y-2'>
                    <InputFormField
                        form={form}
                        t={t}
                        name='number'
                        lang='number'
                    />
                    <DatePicker
                        form={form}
                        t={t}
                        label='Выберите дату начала'
                        lang='fromDate'
                        name='fromDate'
                        disabled={(date: Date) => date < new Date()}
                    />
                    <DatePicker
                        form={form}
                        t={t}
                        label='Выберите дату начала'
                        lang='toDate'
                        name='toDate'
                        disabled={(date: Date) => date < new Date()}
                    />
                    <RangerCombobox form={form} name='receivedId' />
                    <LandCombobox form={form} name='landId' />
                    <AnimalCombobox form={form} name='animalId' />
                    <InputFormField
                        form={form}
                        t={t}
                        name='couponsNumber'
                        lang='couponsNumber'
                        type='number'
                    />
                </div>
                <Button type='submit' className='w-full'>
                    Добавить
                </Button>
            </form>
        </Form>
    );
};

export default CreatePermissionForm;
