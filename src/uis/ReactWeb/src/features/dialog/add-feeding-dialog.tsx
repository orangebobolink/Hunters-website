import {z} from 'zod';
import {useCallback, useState} from 'react';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {Feeding} from '@/entities/feeding/models/Feeding.ts';
import {FeedingProduct} from '@/entities/feeding/models/FeedingProduct.ts';
import {Land} from '@/entities/land/Land.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Status} from '@/entities/status/Status.ts';
import {FeedingService} from '@/entities/feeding/api/FeedingService.ts';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {Button, Form} from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import {useTranslation} from 'react-i18next';
import DatePicker from '@/features/form/date-picker.tsx';
import {RangerCombobox} from '@/features/combobox/ranger-combobox.tsx';
import {LandCombobox} from '@/features/combobox/land-combobox.tsx';
import FeedingProductDrawer from '@/features/drawer/feeding-product-drawet.tsx';
import {Textarea} from '@/shared/ui/textarea.tsx';

const formSchema = z.object({
    number: z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }).min(5, "Номер дожен быть юольше 5 символов"),
    feedingDate: z.date({
        required_error: "Дата обязательна",
        invalid_type_error: "Поле обязано быть строкой",
      }),
    receivedId: z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }),
    landId: z.string({
        required_error: "Поле обязательно",
        invalid_type_error: "Поле обязано быть строкой",
      }),
});

interface IProps
{
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const AddFeedingDialog = ({isOpen, setIsOpen}:IProps) => {
    const { id } = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.create"
        });
    const [feedingProducts, setFeedingProducts] = useState<FeedingProduct[]>([]);
    const [isDrawerOpen, setIsDrawerOpen] = useState<boolean>(false);

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            const status = values.receivedId.length == 0 ? Status.Compiled.toString() : Status.Given.toString();

            const request:Feeding = {
                number: values.number,
                issuedId: id!,
                feedingDate:values.feedingDate,
                receivedId: values.receivedId,
                products: feedingProducts,
                landId: values.landId,
                land:{id: values.landId} as Land,
                receivedDate: new Date(),
                status: status
            }

            try {
                const data = await FeedingService.create(request);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Подкормка созданна успешно",
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
    })

    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                        <div className="flex flex-col justify-around space-y-2">
                            <InputFormField form={form} t={t}
                                            name="number"
                                            lang="number"/>
                            <DatePicker form={form}
                                        t={t}
                                        label="Выберите дату подкормки"
                                        lang="feedingDate"
                                        name="feedingDate"
                                        disabled= {(date: Date) => date < new Date()}/>
                            <Textarea value={feedingProducts.map(f=> f.product == "Seed" ? "Пшеница" : "Овес")}
                                      disabled
                                      className="resize-none text-black"/>
                            <FeedingProductDrawer feedingProducts={feedingProducts}
                                                  setFeedingProducts={setFeedingProducts}
                                                  isOpen={isDrawerOpen}
                                                  setIsOpen={setIsDrawerOpen}
                                                  parentForm={form}
                            />
                            <RangerCombobox form={form}
                                            name="receivedId"/>
                            <LandCombobox form={form}
                                          name="landId"/>
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

export default AddFeedingDialog;