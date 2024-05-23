import { UserService } from '@/entities/user/api/UserService';
import InputFormField from '@/features/form/input-form-field';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { Button } from '@/shared/ui/button';
import { Form } from '@/shared/ui/form';
import { toast } from '@/shared/ui/use-toast';
import { zodResolver } from '@hookform/resolvers/zod';
import { useCallback } from 'react';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { z } from 'zod';

const formSchema = z.object({
    password: z.string().min(5, 'Номер дожен быть юольше 5 символов'),
});

const SettingPage = () => {
    const { id } = useAppSelector(selectAuth);
    const { t } = useTranslation('translation', {
        keyPrefix: 'feeding.create',
    });

    const onSubmit = useCallback(async (values: z.infer<typeof formSchema>) => {
        try {
            const data = await UserService.updatePassword(id, values.password);

            if (data.status >= 200 && data.status <= 300) {
                toast({
                    variant: 'success',
                    title: 'Пароль обновлен успешно',
                });
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
        <div className='w-full flex flex-col justify-center items-center'>
            <div className='w-1/2 m-5'>
                <Form {...form}>
                    <form
                        onSubmit={form.handleSubmit(onSubmit)}
                        className='space-y-8 w-full'
                    >
                        <div className='flex flex-col justify-around space-y-2'>
                            <InputFormField
                                form={form}
                                t={t}
                                name='password'
                                lang='password'
                                type='password'
                            />
                        </div>
                        <Button type='submit' className='w-full'>
                            Добавить
                        </Button>
                    </form>
                </Form>
            </div>
        </div>
    );
};

export default SettingPage;
