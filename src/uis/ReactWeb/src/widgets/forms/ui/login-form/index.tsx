import {
    Button,
    Form,
    FormControl, FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
    Input
} from '@/shared/ui';
import logo from '@/assets/logo.png';
import {useForm} from 'react-hook-form';
import { zodResolver } from "@hookform/resolvers/zod"
import { z } from "zod"
import {LoginRequest} from '@/shared/model/store/queries/typing/requests/LoginRequest.ts';
import {useCallback, useEffect, useMemo} from 'react';
import {useActions} from '@/shared/lib/hooks/useActions.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Notice} from '@/shared/const';
import {toastError, toastSuccess} from '@/shared/lib/utils/ToastUtils.ts';
import {Link, useNavigate} from 'react-router-dom';
import {useTranslation} from 'react-i18next';
import InputFormField from '@/features/form/input-form-field.tsx';

const formSchema = z.object({
    email: z.string().email(),
    password: z.string(),
});

export const LoginForm = () => {
    const { isAuth, error } = useAppSelector(selectAuth);
    const { login, resetStatuses } = useActions();
    const controller = useMemo(() => new AbortController(), []);
    const navigate = useNavigate();
    const { t} = useTranslation("translation",
                                                {
                                                    keyPrefix: "login"
                                                });

    useEffect(() => {
        if (isAuth) {
            toastSuccess(Notice.AUTH_SUCCESSFUL);
            navigate("/");
        } else if (error) toastError(error);
    }, [error, isAuth]);

    useEffect(() => {
        resetStatuses();
        return () => controller.abort();
    }, [resetStatuses, controller]);


    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            email: "",
            password: "",
        },
    })

    const onSubmit = useCallback(
        (values:  z.infer<typeof formSchema>) => {
            const request:LoginRequest = {
                email: values.email,
                password: values.password
            }

            login({ controller, ...request });
        },
        [controller, login],
    );

    if (isAuth) return null;

    return (
        <div className="flex flex-col items-center border-[1px] border-gray-600/30 p-5 w-1/4
                        backdrop-blur-xl bg-green-700/60 rounded-2xl">
            <img src={logo} className="size-[7rem]" alt="logo"/>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full flex flex-col items-center">
                    <div className="w-full">
                        <InputFormField form={form} t={t}
                                        name="email"
                                        lang="email"
                                        type="email"/>

                        <InputFormField form={form} t={t}
                                        name="password"
                                        lang="password"
                                        type="password"/>
                    </div>
                    <div className="w-full flex flex-col">
                        <Button type="submit" className="w-full">
                            {t("signIn")}
                        </Button>
                        <Button variant="link">
                            <Link to="/registration">
                                {t("signUp")}
                            </Link>
                        </Button>
                    </div>
                </form>
            </Form>

        </div>
    );
};
