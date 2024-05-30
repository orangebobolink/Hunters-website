import {useCallback, useEffect, useState} from 'react';
import {Drawer, DrawerClose, DrawerContent, DrawerFooter, DrawerTrigger} from '@/shared/ui/drawer.tsx';
import {Button, Form} from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import {useForm, UseFormReturn} from 'react-hook-form';
import {z} from 'zod';
import {zodResolver} from '@hookform/resolvers/zod';
import {FeedingProduct} from '@/entities/feeding/models/FeedingProduct.ts';
import {useTranslation} from 'react-i18next';
import SelectForm from '@/features/form/select-form.tsx';
import {ProductService} from '@/entities/feeding/api/ProductService.ts';

const formSchema = z.object({
    name: z.string().min(1, "Номер дожен быть юольше 1 символов"),
    unitOfMeasurement: z.string(),
    quantity: z.number(),
});

interface IProps
{
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void,
    parentForm: UseFormReturn<any>,
    feedingProducts: FeedingProduct[],
    setFeedingProducts: (products:FeedingProduct[])=>void
}

const FeedingProductDrawer = ({isOpen, setIsOpen, parentForm, feedingProducts, setFeedingProducts}:IProps) => {
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.create"
        });
    const [products, setProducts] = useState<string[]>([])

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await ProductService.getAll();
                setProducts(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchProducts();
    }, []);

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
           const feedingProduct:FeedingProduct = {
               product: values.name,
               quantity: Number(values.quantity),
               unitOfMeasurement:values.unitOfMeasurement
           }
            console.log(feedingProduct)
           parentForm.setValue("products", [values.name]);
           feedingProducts.push(feedingProduct)
           setFeedingProducts(feedingProducts);
           setIsOpen(false);
           console.log(feedingProducts)
        },
        [],
    );

    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            name: "",
            unitOfMeasurement: "",
            quantity: "",
        },
    })

    const options: { key: string; value: string; }[] = products.map(product => ({
        key: product,
        value: (product == "Seed"?"Пшеница" : "Овес")
      }));
    const optionsMesuare = [
        {key: "кг.", value: "кг."},
        {key: "шт.", value: "шт."},
    ]

    return (
        <Drawer open={isOpen} onOpenChange={setIsOpen}>
            <DrawerTrigger asChild>
                <Button variant="outline">Добавить продукт</Button>
            </DrawerTrigger>
            <DrawerContent>
                <div className="w-full flex justify-center">
                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)} className="w-1/2 space-y-8">
                            <div className="flex flex-col justify-around space-y-2">
                                <SelectForm form={form} t={t}
                                            name="name"
                                            lang="name"
                                            options={options}/>
                                <SelectForm form={form} t={t}
                                                name="unitOfMeasurement"
                                                lang="unitOfMeasurement"
                                                options={optionsMesuare}/>
                                <InputFormField form={form} t={t}
                                                name="quantity"
                                                lang="quantity"
                                                type="number"/>
                            </div>
                            <Button type="submit" className="w-full">
                                {t("add")}
                            </Button>
                        </form>
                    </Form>
                </div>
                <DrawerFooter className="pt-2 flex items-center justify-center w-full">
                    <DrawerClose asChild className="w-1/2">
                        <Button variant="outline">{t("cancel")}</Button>
                    </DrawerClose>
                </DrawerFooter>
            </DrawerContent>
        </Drawer>
    );
};

export default FeedingProductDrawer;