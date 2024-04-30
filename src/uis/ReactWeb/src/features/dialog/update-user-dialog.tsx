import {useCallback, useEffect, useState} from 'react';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {useTranslation} from 'react-i18next';
import {z} from 'zod';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {UserService} from '@/entities/user/UserService.ts';
import {Button, Form, FormControl, FormField, FormItem, FormLabel} from '@/shared/ui';
import {ToggleGroup, ToggleGroupItem} from '@/shared/ui/toggle-group.tsx';
import {useToast} from '@/shared/ui/use-toast.ts';
import {User} from '@/entities/user/User.ts';

const formSchema = z.object({
    roleNames: z.string().array().nonempty("Роли обязательны")
});

interface IProps
{
    user: User,
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const UpdateUserDialog = ({isOpen, user, setIsOpen}:IProps) => {
    const { toast } = useToast()
    const { t} = useTranslation("translation",
        {
            keyPrefix: "registration"
        });

    const options = [
        { label: "Охотник", value: "User" },
        { label: "Егерь", value: "Ranger" },
        { label: "Охотовед", value: "Manager" },
        { label: "Директор", value: "Director" },
        { label: "Администратор", value: "Admin" },
    ];

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            console.log(user)
            user.roleNames = values.roleNames
            console.log(user)

            try {
                const data = await UserService.update(user);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Пользователь обновлен успешно",
                    })
                    setIsOpen(false)
                }
            }catch {
                toast({
                    variant: "destructive",
                    title: "Что-то пошло не так",
                })
            }

        },
        [user, toast, setIsOpen],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            roleNames: user?.roleNames
        },
    })

    const userRoleNames: [string, ...string[]] = user && user.roleNames && user.roleNames.length > 0
                                                 ? [user.roleNames[0], ...user.roleNames.slice(1)]
                                                 : ['defaultRole'];
    useEffect(() => {
        if (user?.roleNames) {
            form.setValue('roleNames', userRoleNames);
        }
    }, [user?.roleNames, form]);

    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                        <FormField
                            control={form.control}
                            name="roleNames"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel className="text-green-500">
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
                        <Button type="submit" className="w-full">
                            Обновить
                        </Button>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    );
};

export default UpdateUserDialog;