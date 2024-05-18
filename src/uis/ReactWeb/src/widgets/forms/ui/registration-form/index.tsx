import {
    Button,
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from '@/shared/ui';
import logo from '@/assets/logo.png';
import {useForm} from 'react-hook-form';
import { zodResolver } from "@hookform/resolvers/zod"
import { z } from "zod"
import {useCallback, useEffect, useState} from 'react';
import { format } from "date-fns"
import {Notice} from '@/shared/const';
import {toastError, toastSuccess} from '@/shared/lib/utils/ToastUtils.ts';
import {Link, useNavigate} from 'react-router-dom';
import {useTranslation} from 'react-i18next';
import InputFormField from '@/features/form/input-form-field.tsx';
import {ErrorUtils} from '@/shared/lib/utils/ErrorUtils.ts';
import {RegisterRequest} from '@/shared/model/store/queries/typing/requests/RegisterRequest.ts';
import {useRegisterMutation} from '@/shared/model/store/queries/auth.rtk.ts';
import {Sex} from '@/shared/model/store/queries/typing/requests/Sex.ts';
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from '@/shared/ui/select.tsx';
import {cn} from '@/shared/lib';
import {CalendarIcon} from '@radix-ui/react-icons';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {Calendar} from '@/shared/ui/calendar.tsx';
import DatePicker from '@/features/form/date-picker.tsx';
import SelectForm from '@/features/form/select-form.tsx';

const formSchema = z.object({
    email: z.string()
        .email("This is not a valid email."),
    phoneNumber: z.string(),
    password: z.string(),
    firstName: z.string().min(2).max(50),
    lastName: z.string().min(2).max(50),
    dateOfBirth: z.date({
        required_error: "A date of birth is required.",
    }),
    sex:z.string().min(2).max(50),
});

export const RegistrationForm = () => {
    const navigate = useNavigate();
    const [register, { isSuccess, isError, error }] =
        useRegisterMutation();
    const { t} = useTranslation("translation",
        {
            keyPrefix: "registration"
        });
    const options: string[] = [Sex.Male.toString(), Sex.Female.toString()];
    const [currentRequest, setCurrentRequest] = useState<{
        abort: () => void;
    } | null>(null);

    const onSubmit = useCallback(
        (values:  z.infer<typeof formSchema>) => {
            console.log(values)
            const request:RegisterRequest = {
                email: values.email,
                password: values.password,
                phoneNumber: values.phoneNumber,
                firstName: values.firstName,
                lastName: values.lastName,
                dateOfBirth: values.dateOfBirth,
                sex: values.sex as Sex,
            }

            if (currentRequest) currentRequest.abort();
            const data =register(request);

            setCurrentRequest(data);
        },
        [currentRequest, register],
    );

    useEffect(() => {
        if (isSuccess) {
            toastSuccess(Notice.REGISTRATION_SUCCESSFUL);
            navigate("/login");
        } else if (isError && ErrorUtils.isTypedError(error)) {
            toastError(error.data.message);
        } else if (isError) toastError(Notice.UNEXPECTED_ERROR);
    }, [error, isError, isSuccess]);

    useEffect(
        () => () => {
            if (currentRequest) currentRequest.abort();
        },
        [currentRequest],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            email: "",
            phoneNumber: "",
            password: "",
            firstName: "",
            lastName: "",
            dateOfBirth: new Date(),
            sex:"",
        },
    })

    return (
        <div className="flex flex-col items-center border-[1px] border-gray-600/30 p-5 w-1/3
                        backdrop-blur-xl bg-green-700/60 rounded-2xl">
            <img src={logo} className="size-[7rem]" alt="logo"/>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                    <div className="flex flex-row justify-around">
                        <div className="space-y-2">
                            <InputFormField form={form} t={t}
                                            name="email"
                                            lang="email"
                                            type="email"/>
                            <InputFormField form={form} t={t}
                                            name="phoneNumber"
                                            lang="phone"
                                            type="phone"/>

                            <InputFormField form={form} t={t}
                                            name="password"
                                            lang="password"
                                            type="password"/>
                        </div>
                        <div className="space-y-2">
                            <InputFormField form={form} t={t}
                                            name="firstName"
                                            lang="firstName"/>
                            <InputFormField form={form} t={t}
                                            name="lastName"
                                            lang="lastName"/>
                            <DatePicker form={form}
                                        t={t}
                                        label="Ваша дата рождения"
                                        lang="animal.name"
                                        name="dateOfBirth"
                                        disabled= {(date) =>
                                            date > new Date() || date < new Date("1900-01-01")}
                            />
                            <SelectForm  t={t}
                                         form={form}
                                         name="sex"
                                         lang="sex"
                                         options= {options}
                           />
                        </div>
                    </div>
                    <Button type="submit" className="w-full">
                        {t("signUp")}
                    </Button>
                </form>
            </Form>
            <Button variant="link">
                <Link to="/login">
                    {t("alreadyHaveAccount")}
                </Link>
            </Button>
        </div>
    );
};
