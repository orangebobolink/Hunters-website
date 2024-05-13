import {useCallback} from 'react';
import {useTranslation} from 'react-i18next';
import {z} from 'zod';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {Animal} from '@/entities/animal/models/Animal.ts';
import {AnimalService} from '@/entities/animal/api/AnimalService.ts';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {Button, Form, FormControl, FormField, FormItem, FormLabel, FormMessage} from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import {Select, SelectContent, SelectItem, SelectTrigger, SelectValue} from '@/shared/ui/select.tsx';

enum AnimalType
{
    Mammal,
    Bird
}

interface IProps
{
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const formSchema = z.object({
    name: z.string(),
    type: z.string(),
    description: z.string(),
    imageUrl: z.string(),
});


const AddAnimalDialog = ({isOpen, setIsOpen}:IProps) => {
    const { t} = useTranslation("translation");

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            console.log(values)
            const request:Animal = {
                type: values.type,
                name: values.name,
                description: values.description,
                imageUrl: values.imageUrl,
            }

            try {
                const data = await AnimalService.create(request);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Сезон охоты добавлен успешно",
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
        [],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: "",
            type: "",
            description:"",
            imageUrl: "",
        },
    })

    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                        <div className="flex flex-col justify-around">
                            <InputFormField form={form} t={t}
                                            name="name"
                                            lang="animal.name"/>
                            <InputFormField form={form} t={t}
                                            name="description"
                                            lang="animal.description"/>
                            <FormField
                                control={form.control}
                                name="type"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel className="text-green-500">
                                            {t("registration.sex")}
                                        </FormLabel>
                                        <FormControl className="border-black/50">
                                            <Select onValueChange={field.onChange} defaultValue={field.value}>
                                                <SelectTrigger className="w-[180px]">
                                                    <SelectValue placeholder="Пол" />
                                                </SelectTrigger>
                                                <SelectContent>
                                                    <SelectItem value={AnimalType.Mammal.toString()}>Животное</SelectItem>
                                                    <SelectItem value={AnimalType.Bird.toString()}>Птица</SelectItem>
                                                </SelectContent>
                                            </Select>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <InputFormField form={form} t={t}
                                            name="imageUrl"
                                            lang="animal.description"
                                            type="file"/>
                        </div>
                        <Button type="submit" className="w-full">
                            Добавить
                        </Button>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    );
};

export default AddAnimalDialog;