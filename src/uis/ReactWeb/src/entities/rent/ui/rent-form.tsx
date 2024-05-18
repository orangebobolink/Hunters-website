import {useCallback, useState} from 'react';
import {z} from 'zod';
import {useTranslation} from 'react-i18next';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {Product} from '@/entities/rent/models/Product.ts';
import {ProductService} from '@/entities/rent/api/ProductService.ts';
import {Button, Form} from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import SelectForm from '@/features/form/select-form.tsx';
import {Type} from '@/entities/rent/models/Type.ts';

const formSchema = z.object({
    name: z.string(),
    price: z.number(),
    description: z.string(),
    quantityInStock: z.number(),
    imageUrl: z.string(),
    type:z.string(),
});

function stringToMyEnum(value: string): Type {
    return Type[value as keyof typeof Type];
}

interface IProps {
    selectedProduct: Product | null,
    type?:string
}

const RentForm = ({selectedProduct, type="add"} : IProps) => {
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.create"
        });

    const [product, setProduct] = useState<Product>(
        () : Product => {
            if(selectedProduct == null)
            {
                return {
                    name: "",
                    price: 0,
                    quantityInStock: 0,
                    description: "",
                    imageUrl: "",
                    type:Type.Gun,
                }
            }
            return selectedProduct
        }
    )

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            if(type == "add") {
                const request: Product = {
                    name: values.name,
                    price: values.price,
                    description: values.description,
                    quantityInStock: values.quantityInStock,
                    imageUrl: values.imageUrl,
                    type: stringToMyEnum(values.type)
                }

                try {
                    const data = await ProductService.create(request);

                    if (data.status >= 200 && data.status <= 300) {
                        toast({
                            variant: "success",
                            title: "Продукт созданна успешно",
                        })
                    }
                } catch {
                    toast({
                        variant: "destructive",
                        title: "Что-то пошло не так",
                    })
                }
            } else if(type =="update") {
                const request: Product = {
                    id:selectedProduct?.id,
                    name: values.name,
                    price: values.price,
                    description: values.description,
                    quantityInStock: values.quantityInStock,
                    imageUrl: values.imageUrl,
                    type: stringToMyEnum(values.type)
                }

                try {
                    const data = await ProductService.update(request);

                    if (data.status >= 200 && data.status <= 300) {
                        toast({
                            variant: "success",
                            title: "Продукт успешно обнавленн",
                        })
                    }
                } catch {
                    toast({
                        variant: "destructive",
                        title: "Что-то пошло не так",
                    })
                }
            }
        },
        [],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: selectedProduct?.name,
            price: selectedProduct?.price,
            quantityInStock: selectedProduct?.quantityInStock,
            description: selectedProduct?.description,
            imageUrl: "",
            type:selectedProduct?.type.toString()
        },
    })

    const options = [
        Type[Type.Car],
        Type[Type.Ammunition],
        Type[Type.Gun]
    ]

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                <div className="flex flex-col justify-around space-y-2">
                    <InputFormField form={form} t={t}
                                    name="name"
                                    lang="name"/>
                    <InputFormField form={form} t={t}
                                    name="description"
                                    lang="description"/>
                    <InputFormField form={form} t={t}
                                    name="price"
                                    lang="price"
                                    type="number"/>
                    <InputFormField form={form} t={t}
                                    name="quantityInStock"
                                    lang="quantityInStock"
                                    type="number"/>
                    <InputFormField form={form} t={t}
                                    name="imageUrl"
                                    lang="imageUrl"
                                    type="file"/>
                    <SelectForm t={t}
                                form={form}
                                name="type"
                                lang="type"
                                options={options}/>
                </div>
                <Button type="submit" className="w-full">
                    {type === 'add'
                     ?
                     <span>Добавить</span>
                     :
                     <span>Обновить</span>
                    }
                </Button>
            </form>
        </Form>
    );
};

export default RentForm;