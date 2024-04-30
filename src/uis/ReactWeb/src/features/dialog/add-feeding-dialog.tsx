import {z} from 'zod';
import {useCallback} from 'react';
import {Animal} from '@/entities/animal/Animal.ts';
import {AnimalService} from '@/entities/animal/AnimalService.ts';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';

const formSchema = z.object({
    email: z.string()
        .email("This is not a valid email."),
    phoneNumber: z.string(),
    password: z.string(),
    userName: z.string(),
    firstName: z.string().min(2).max(50),
    lastName: z.string().min(2).max(50),
    middleName: z.string().min(2).max(50),
    dateOfBirth: z.date({
        required_error: "A date of birth is required.",
    }),
    sex:z.string().min(2).max(50),
    roleNames: z.string().array().nonempty("Роли обязательны")
});

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


const AddFeedingDialog = ({isOpen, setIsOpen}:IProps) => {
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
        <div>

        </div>
    );
};

export default AddFeedingDialog;