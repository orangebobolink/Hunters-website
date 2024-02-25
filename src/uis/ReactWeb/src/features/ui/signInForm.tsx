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

const formSchema = z.object({
    username: z.string().min(2).max(50),
    password: z.string(),
});

export const SignInForm = () => {
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            username: "",
            password: "",
        },
    })

    function onSubmit(values: z.infer<typeof formSchema>) {
        // Do something with the form values.
        // ✅ This will be type-safe and validated.
        console.log(values)
    }

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
                                <FormLabel className="text-green-500">Username</FormLabel>
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
                                <FormLabel className="text-black">Password</FormLabel>
                                <FormControl className="border-black/50">
                                    <Input type="password" {...field} />
                                </FormControl>
                                <FormDescription>
                                    <Button variant="link">Забыли пароль?</Button>
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                    <Button type="submit" className="w-full">Submit</Button>
                </form>
            </Form>
            <Button variant="link">Регистрация</Button>
        </div>
    );
};
