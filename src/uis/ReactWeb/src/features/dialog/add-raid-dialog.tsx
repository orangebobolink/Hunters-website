import {z} from 'zod';
import {useCallback, useState} from 'react';
import {toast} from '@/shared/ui/use-toast.ts';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import {Land} from '@/entities/land/Land.ts';
import {Status} from '@/entities/status/Status.ts';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {Button, Form} from '@/shared/ui';
import InputFormField from '@/features/form/input-form-field.tsx';
import {useTranslation} from 'react-i18next';
import DatePicker from '@/features/form/date-picker.tsx';
import {LandCombobox} from '@/features/combobox/land-combobox.tsx';
import {Raid} from '@/entities/raid/Raid.ts';
import MultyRangerCombobox from '@/features/combobox/multy-ranger-combobox.tsx';
import {RaidService} from '@/entities/raid/RaidService.ts';
import {User} from '@/entities/user/models/User.ts';

const formSchema = z.object({
    exitTime: z.date(),
    returnedTime: z.date(),
    participant: z.array(z.object({})),
    landId: z.string(),
    note: z.string(),
});

interface IProps
{
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const AddRaidDialog = ({isOpen, setIsOpen}:IProps) => {
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.create"
        });
    const [participants, setParticipants] = useState<User[]>([]);

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            const status = Status.Completed.toString()
            console.log(participants)
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
                const data = await RaidService.create(request);

                if(data.status >= 200 && data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Рейд создан успешно",
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
        [participants],
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
                            <MultyRangerCombobox form={form}
                                                 t={t}
                                                 lang="rangers"
                                                 name="participant"
                                                 setParticipant={setParticipants}
                                                />
                            <LandCombobox form={form}
                                          name="landId"/>
                            <InputFormField form={form} t={t}
                                            name="note"
                                            lang="note"/>
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