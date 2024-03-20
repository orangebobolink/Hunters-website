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

const formSchema = z.object({
    username: z.string().min(2).max(50),
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

        } else if (error) toastError(error);
    }, [error, isAuth]);

    useEffect(() => {
        resetStatuses();
        return () => controller.abort();
    }, [resetStatuses, controller]);


    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            username: "",
            password: "",
        },
    })

    const onSubmit = useCallback(
        (values:  z.infer<typeof formSchema>) => {
            const request:LoginRequest = {
                username: values.username,
                password: values.password
            }

            login({ controller, ...request });
            navigate("/");
        },
        [controller, login],
    );

    if (isAuth) return null;

    return (
        <div className="flex flex-col items-center border-[1px] border-gray-600/30 p-5 w-1/4
                        backdrop-blur-xl bg-green-500/40 rounded-2xl">
            <img src={logo} className="size-[7rem]" alt="logo"/>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                    <FormField
                        control={form.control}
                        name="username"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel className="text-green-500">
                                    {t("username")}
                                </FormLabel>
                                <FormControl className="border-black/50">
                                    <Input {...field} />
                                </FormControl>
                                <FormMessage />
                            </FormItem>
                        )}
                    />

                    <FormField
                        control={form.control}
                        name="password"
                        render={({ field }) => (
                            <FormItem>
                                <FormLabel className="text-black">
                                    {t("password")}
                                </FormLabel>
                                <FormControl className="border-black/50">
                                    <Input type="password" {...field} />
                                </FormControl>
                                <FormDescription>
                                    <Button variant="link">
                                        {t("forgotPassword")}
                                    </Button>
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <Button type="submit" className="w-full">
                        {t("signIn")}
                    </Button>
                </form>
            </Form>
            <Button variant="link">
                <Link to="/registration">
                    {t("signUp")}
                </Link>
            </Button>
        </div>
    );
};
