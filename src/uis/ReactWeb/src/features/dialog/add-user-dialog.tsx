import {useCallback, useEffect, useState} from 'react';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {useTranslation} from 'react-i18next';
import {date, z} from 'zod';
import {Sex} from '@/shared/model/store/queries/typing/requests/Sex.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {UserService} from '@/entities/user/api/UserService.ts';
import {CreateUser} from '@/entities/user/models/CreateUser.ts';
import InputFormField from '@/features/form/input-form-field.tsx';
import {Button, Form, FormControl, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from '@/shared/ui/select.tsx';
import {ToggleGroup, ToggleGroupItem} from '@/shared/ui/toggle-group.tsx';
import {useToast} from '@/shared/ui/use-toast.ts';
import DatePicker from '../form/date-picker';

const formSchema = z.object({
    email:  z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      })
        .email("Данная строка не явялется электронной почтой"),
    phoneNumber:  z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }),
    password:z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }),
    userName: z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }),
    firstName: z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }).min(2, {message:"Имя меньше чем 2 буквы"}).max(50),
    lastName: z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
    }).min(2, {message:"Фамилия меньше чем 2 буквы"}).max(50),
    middleName:z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
    }).min(2, {message:"Отчество меньше чем 2 буквы"}).max(50),
    dateOfBirth: z.date({
        required_error: "День рождения обязателен.",
    }),
    sex:z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
    }).min(2, {message:"Пол меньше чем 2 буквы"}).max(50),
    roleNames: z.string().array().nonempty("Роли обязательны")
});

interface IProps
{
    increaseCount:()=>void,
    isOpen: boolean,
    setIsOpen:(flag:boolean)=>void
}

const AddUserDialog = ({isOpen, setIsOpen, increaseCount}:IProps) => {
    const { toast } = useToast()
    const { t} = useTranslation("translation");

    const [currentRequest, setCurrentRequest] = useState<{
        abort: () => void;
    } | null>(null);

    const options = [
        { label: "Охотник", value: "User" },
        { label: "Егерь", value: "Ranger" },
        { label: "Охотовед", value: "Manager" },
        { label: "Директор", value: "Director" },
        { label: "Администратор", value: "Admin" },
    ];

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            console.log(values)
            const request:CreateUser = {
                email: values.email,
                phoneNumber: values.phoneNumber,
                userName: values.userName,
                password: values.password,
                firstName: values.firstName,
                lastName: values.lastName,
                middleName: values.middleName,
                dateOfBirth: values.dateOfBirth.toString(),
                sex: values.sex as Sex,
                roleNames: values.roleNames
            }

            if (currentRequest) currentRequest.abort();

            try {
                const data = await UserService.create(request);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Пользователь добавлен успешно",
                    })
                    setIsOpen(false)
                    increaseCount()
                }
            }catch {
                toast({
                    variant: "destructive",
                    title: "Что-то пошло не так",
                })
            }

        },
        [currentRequest],
    );

    useEffect(
        () => () => {
            if (currentRequest) currentRequest.abort();
        },
        [currentRequest],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            dateOfBirth: new Date(),
            roleNames: []
        },
    })
    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                        <div className="flex flex-col justify-around">
                            <div className="flex flex-row items-center justify-between">
                                <div>
                                    <InputFormField form={form} t={t}
                                                    name="email"
                                                    lang="registration.email"
                                                    type="email"/>
                                    <InputFormField form={form} t={t}
                                                    name="phoneNumber"
                                                    lang="registration.phone"
                                                    type="phone"/>
                                    <InputFormField form={form} t={t}
                                                    name="userName"
                                                    lang="registration.userName"/>
                                    <InputFormField form={form} t={t}
                                                    name="password"
                                                    lang="registration.password"
                                                    type="password"/>
                                </div>
                                <div>
                                    <InputFormField form={form} t={t}
                                                    name="firstName"
                                                    lang="registration.firstName"/>
                                    <InputFormField form={form} t={t}
                                                    name="lastName"
                                                    lang="registration.lastName"/>
                                    <InputFormField form={form} t={t}
                                                    name="middleName"
                                                    lang="registration.middleName"/>
                                    <DatePicker form={form}
                                        t={t}
                                        label="Дата пождения"
                                        lang="dateOfBirth"
                                        name="dateOfBirth"
                                        disabled= {(date:Date)=> date > new Date() || date < new Date("1900-01-01")}/>
                                  
                                    <FormField
                                        control={form.control}
                                        name="sex"
                                        render={({ field }) => (
                                            <FormItem>
                                                <FormLabel  >
                                                    {t("registration.sex")}
                                                </FormLabel>
                                                <FormControl className="border-black/50">
                                                    <Select onValueChange={field.onChange} defaultValue={field.value}>
                                                        <SelectTrigger className="w-[180px]">
                                                            <SelectValue placeholder="Пол" />
                                                        </SelectTrigger>
                                                        <SelectContent>
                                                            <SelectItem value={Sex.Male.toString()}>Мужской</SelectItem>
                                                            <SelectItem value={Sex.Female.toString()}>Женский</SelectItem>
                                                        </SelectContent>
                                                    </Select>
                                                </FormControl>
                                                <FormMessage />
                                            </FormItem>
                                        )}
                                    />
                                </div>
                            </div>
                            <div>
                                <FormField
                                    control={form.control}
                                    name="roleNames"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>
                                                Роли
                                            </FormLabel>
                                            <FormControl>
                                                <ToggleGroup
                                                    type="multiple"
                                                    value={field.value}
                                                    onValueChange={field.onChange}
                                                >
                                                    {options.map((option) => (
                                                        <ToggleGroupItem key={option.value} value={option.value}>
                                                            {option.label}
                                                        </ToggleGroupItem>
                                                    ))}
                                                </ToggleGroup>
                                            </FormControl>
                                        </FormItem>
                                    )}
                                />
                            </div>
                        </div>
                        <Button type="submit" className="w-full">
                            Создать
                        </Button>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    );
};

export default AddUserDialog;