import {Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle} from '@/shared/ui/dialog.tsx';
import {AspectRatio} from '@/shared/ui/aspect-ratio.tsx';
import {Animal} from '@/entities/animal/Animal.ts';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {HuntingSeason} from '@/entities/huntinSeason/HuntingSeason.ts';
import {format} from 'date-fns';
import {Button, Form, FormControl, FormField, FormLabel} from '@/shared/ui';
import {Tabs, TabsContent, TabsList, TabsTrigger} from '@/shared/ui/tabs.tsx';
import {z} from 'zod';
import React, {useCallback} from 'react';
import {useForm} from 'react-hook-form';
import {zodResolver} from '@hookform/resolvers/zod';
import InputFormField from '@/features/form/input-form-field.tsx';
import {Popover, PopoverContent, PopoverTrigger} from '@/shared/ui/popover.tsx';
import {cn} from '@/shared/lib';
import {CalendarIcon} from '@radix-ui/react-icons';
import {Calendar} from '@/shared/ui/calendar.tsx';
import {useTranslation} from 'react-i18next';
import {toast} from '@/shared/ui/use-toast.ts';
import {HuntingService} from '@/entities/huntinSeason/HuntingSeasonService.ts';

interface IProps
{
    animal: Animal,
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const formSchema = z.object({
    startDate: z.date({
        required_error: "A start date is required.",
    }),
    endDate: z.date({
        required_error: "A end date is required.",
    }),
    weapon: z.string(),
    note: z.string(),
    wayOfHunting: z.string()
});


const AnimalInfoDialog = ({animal, isOpen, setIsOpen}:IProps) => {
    const { t} = useTranslation("translation");

    const handleRemove = async (id:string)=>{
        animal.huntingSeasons = animal.huntingSeasons?.filter((h)=> h.id != id);
        await HuntingService.remove(id);
    }

    const onSubmit = useCallback(
        async (values:  z.infer<typeof formSchema>) => {
            console.log(values)
            const request:HuntingSeason = {
                animalId: animal.id!,
                startDate: values.startDate,
                endDate: values.endDate,
                note: values.note,
                wayOfHunting: values.wayOfHunting,
                weapon: values.weapon,
            }
            animal.huntingSeasons?.push(request);

            try {
                const data = await HuntingService.create(request);

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
            startDate: new Date(),
            endDate: new Date(),
            weapon: "",
            note: "",
            wayOfHunting: ""
        },
    })

    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <Tabs defaultValue="animal">
                    <TabsList className="grid w-full grid-cols-2">
                        <TabsTrigger value="animal">Информация</TabsTrigger>
                        <TabsTrigger value="huntingSeasson">Добавить сезон охоты</TabsTrigger>
                    </TabsList>
                    <TabsContent value="animal">
                        <DialogHeader>
                            <AspectRatio ratio={16 / 9}>
                                <img src={animal.imageUrl}
                                     alt="Image"
                                     className="rounded-md object-cover" />
                            </AspectRatio>
                            <DialogTitle>{animal.name}</DialogTitle>
                            <DialogDescription>
                                {animal.description}
                            </DialogDescription>
                        </DialogHeader>
                        <Table>

                            <TableHeader>
                                <TableRow>
                                    <TableHead className="w-[100px]">Начало сезона</TableHead>
                                    <TableHead>Конец сезона</TableHead>
                                    <TableHead>Оружие</TableHead>
                                    <TableHead className="text-right">Примечание</TableHead>
                                    <TableHead className="text-right">Действия</TableHead>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {animal.huntingSeasons?.map((huntingSeason: HuntingSeason)=> (
                                    <TableRow key={huntingSeason.id}>
                                        <TableCell className="font-medium">{format(huntingSeason.startDate, "MM/dd/yyyy")}</TableCell>
                                        <TableCell>{format(huntingSeason.endDate, "MM/dd/yyyy")}</TableCell>
                                        <TableCell>{huntingSeason.weapon}</TableCell>
                                        <TableCell className="text-right">{huntingSeason.note}</TableCell>
                                        <TableCell className="text-right">
                                            <Button onClick={()=>{handleRemove(huntingSeason.id!)}}>Удалить</Button>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TabsContent>
                    <TabsContent value="huntingSeasson">
                        <Form {...form}>
                            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8 w-full">
                                <div className="flex flex-col justify-around">
                                    <FormField
                                        control={form.control}
                                        name="startDate"
                                        render={({ field }) => (
                                            <Popover>
                                                <FormLabel className="mb-3 mt-1">
                                                    Дата начала сезона
                                                </FormLabel>
                                                <PopoverTrigger asChild>
                                                    <FormControl>
                                                        <Button
                                                            variant={"outline"}
                                                            className={cn(
                                                                "w-[240px] pl-3 text-left font-normal",
                                                                !field.value && "text-muted-foreground"
                                                            )}
                                                        >
                                                            {field.value ? (
                                                                format(field.value, "PPP")
                                                            ) : (
                                                                 <span>Pick a date</span>
                                                             )}
                                                            <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                                                        </Button>
                                                    </FormControl>
                                                </PopoverTrigger>
                                                <PopoverContent className="w-auto p-0" align="start">
                                                    <Calendar
                                                        mode="single"
                                                        selected={field.value}
                                                        onSelect={field.onChange}
                                                        disabled={(date) =>
                                                             date < new Date("1900-01-01")
                                                        }
                                                        initialFocus
                                                    />
                                                </PopoverContent>
                                            </Popover>
                                        )}
                                    />
                                    <FormField
                                        control={form.control}
                                        name="endDate"
                                        render={({ field }) => (
                                            <Popover>
                                                <FormLabel className="mb-3 mt-1">
                                                    Дата окончания сезона
                                                </FormLabel>
                                                <PopoverTrigger asChild>
                                                    <FormControl>
                                                        <Button
                                                            variant={"outline"}
                                                            className={cn(
                                                                "w-[240px] pl-3 text-left font-normal",
                                                                !field.value && "text-muted-foreground"
                                                            )}
                                                        >
                                                            {field.value ? (
                                                                format(field.value, "PPP")
                                                            ) : (
                                                                 <span>Pick a date</span>
                                                             )}
                                                            <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                                                        </Button>
                                                    </FormControl>
                                                </PopoverTrigger>
                                                <PopoverContent className="w-auto p-0" align="start">
                                                    <Calendar
                                                        mode="single"
                                                        selected={field.value}
                                                        onSelect={field.onChange}
                                                        disabled={(date) =>
                                                            date < form.getValues().startDate
                                                        }
                                                        initialFocus
                                                    />
                                                </PopoverContent>
                                            </Popover>
                                        )}
                                    />
                                    <InputFormField form={form} t={t}
                                                    name="weapon"
                                                    lang="animal.weapon"/>
                                    <InputFormField form={form} t={t}
                                                    name="note"
                                                    lang="animal.note"/>
                                    <InputFormField form={form} t={t}
                                                    name="wayOfHunting"
                                                    lang="animal.wayOfHunting"/>

                                </div>
                                <Button type="submit" className="w-full">
                                    Добавить
                                </Button>
                            </form>
                        </Form>
                    </TabsContent>
                </Tabs>
            </DialogContent>
        </Dialog>
    );
};

export default AnimalInfoDialog;