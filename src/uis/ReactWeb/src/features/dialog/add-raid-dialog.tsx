import {z} from 'zod';
import {useCallback, useState} from 'react';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {Land} from '@/entities/land/Land.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Status} from '@/entities/status/Status.ts';
import {FeedingService} from '@/entities/feeding/FeedingService.ts';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {Button, Form} from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import {useTranslation} from 'react-i18next';
import DatePicker from '@/features/form/date-picker.tsx';
import {RangerCombobox} from '@/features/combobox/ranger-combobox.tsx';
import {LandCombobox} from '@/features/combobox/land-combobox.tsx';
import FeedingProductDrawer from '@/features/drawer/feeding-product-drawet.tsx';
import {Textarea} from '@/shared/ui/textarea.tsx';
import {Raid} from '@/entities/raid/Raid.ts';
import {User} from '@/entities/user/User.ts';
import {UserService} from '@/entities/user/UserService.ts';
import MultyRangerCombobox from '@/features/combobox/multy-ranger-combobox.tsx';

const formSchema = z.object({
    exitTime: z.date(),
    returnedTime: z.date(),
    participants: z.string(),
    landId: z.string(),
    note: z.string(),
});

interface IProps
{
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const AddRaidDialog = ({isOpen, setIsOpen}:IProps) => {
    const { id } = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.create"
        });
    const [participants, setParticipants] = useState<User[]>([]);
    const [isDrawerOpen, setIsDrawerOpen] = useState<boolean>(false);

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            const status = Status.Completed.toString()

            const request:Raid = {
                exitTime: values.exitTime,
                returnedTime: values.returnedTime,
                landId: values.landId,
                land:{id: values.landId} as Land,
                participants: participants,
                status: status,
                note: values.note
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
                                        lang="exitTime"
                                        name="exitTime"
                                        disabled= {(date: Date) => date < new Date()}
                                        time={true}/>
                            <DatePicker form={form}
                                        t={t}
                                        label="Выберите дату подкормки"
                                        lang="returnedTime"
                                        name="returnedTime"
                                        disabled= {(date: Date) => form.getValues().exitTime < date && date < new Date()}
                                        time={true}/>

                            <Textarea value={participants.map(f=> UserService.getFullName(f))}
                                      disabled
                                      className="resize-none"/>
                            <MultyRangerCombobox form={form}
                                                 name="Егря"/>
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

export default AddRaidDialog;