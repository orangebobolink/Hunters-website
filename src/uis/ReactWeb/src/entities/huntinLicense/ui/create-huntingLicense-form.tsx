import React, { useCallback } from 'react';
import { z } from 'zod';
import { useTranslation } from 'react-i18next';
import { toast } from '@/shared/ui/use-toast.ts';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Button, Form } from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import { HuntingLicense } from '@/entities/huntinLicense/HuntingLicense.ts';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { HuntingLicenseService } from '@/entities/huntinLicense/HuntingLicenseService.ts';
import { useActions } from '@/shared/lib/hooks/useActions';

const formSchema = z.object({
    licenseNumber: z.string(),
});

interface IProps {
    setChangeRender: (render: boolean) => void;
}

const CreateHuntingLicenseForm = ({ setChangeRender }: IProps) => {
    const { changeHuntingLicenseId } = useActions();
    const { id } = useAppSelector(selectAuth);
    const { t } = useTranslation('translation', {
        keyPrefix: 'huntingLicense.create',
    });

    const onSubmit = useCallback(async (values: z.infer<typeof formSchema>) => {
        const request: HuntingLicense = {
            userId: id!,
            licenseNumber: values.licenseNumber,
        };

        try {
            const data = await HuntingLicenseService.create(request);

            if (data.status >= 200 && data.status <= 300) {
                toast({
                    variant: 'success',
                    title: 'Охотничья лицензия проверенна успешно',
                });
                console.log(data.data.id);
                changeHuntingLicenseId({ id: data.data.id! });
                setChangeRender(true);
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
                        name='licenseNumber'
                        lang='licenseNumber'
                    />
                </div>
                <Button type='submit' className='w-full'>
                    Добавить
                </Button>
            </form>
        </Form>
    );
};

export default CreateHuntingLicenseForm;
