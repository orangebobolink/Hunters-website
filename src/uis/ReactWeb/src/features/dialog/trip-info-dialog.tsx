import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {format} from 'date-fns';
import {UserService} from '@/entities/user/UserService.ts';
import {Trip} from '@/entities/trip/Trip.ts';
import {Button} from '@/shared/ui';
import {ScrollArea} from '@/shared/ui/scroll-area.tsx';
import {Separator} from '@radix-ui/react-select';
import AddDataEventForm from '@/entities/trip/ui/add-data-event-form.tsx';
import {AxiosResponse} from 'axios';
import {toast} from '@/shared/ui/use-toast.ts';
import {Status} from '@/entities/status/Status.ts';
import {TripService} from '@/entities/trip/TripService.ts';

interface IProps {
    trip: Trip,
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const TripInfoDialog = ({trip, isOpen, setIsOpen}: IProps) => {
    const {roles} = useAppSelector(selectAuth);

    const handler = async (response : AxiosResponse<any>) => {
        if(response.status >= 200 && response.status <= 300)
        {
            toast({
                variant: "success",
                title: "Статус успешно изменен",
            })
            setIsOpen(false)
        }
        else
        {
            toast({
                variant: "destructive",
                title: "Что-то пошло не так",
            })
            setIsOpen(false)
        }
    }

    const handlerUpdateStatus = async () => {
        trip.status = Status[Status.Completed]
        trip.returnedDate = new Date()
        const response = await TripService.update(trip)

        handler(response)
    }

    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <div className="p-4 shadow-md rounded-lg">
                    <p className="font-bold">Номер: <span className="font-normal">{trip.number}</span></p>
                    <p className="font-bold">Дата подкормки: <span className="font-normal">{format(trip.permission!.fromDate, "MM/dd/yyyy")}</span></p>
                    <p className="font-bold">Дата подкормки: <span className="font-normal">{format(trip.permission!.toDate, "MM/dd/yyyy")}</span></p>
                    <p className="font-bold">Составитель: <span className="font-normal">{UserService.getFullName(trip.permission!.issued!)}</span></p>
                    <p className="font-bold">Выданно егерю: <span className="font-normal">{UserService.getFullName(trip.permission!.received!)}</span></p>
                    <p className="font-bold">Состояние: <span className="font-normal">{trip.status}</span></p>
                    <p className="font-bold">Локация: <span className="font-normal">{trip.permission!.land?.name}</span></p>
                    <p className="font-bold">Имя животного: <span className="font-normal">{trip.permission!.animal?.name}</span></p>
                    <p className="font-bold">
                        Дата охоты:
                        <span> </span>
                        {
                            format(trip.eventDate!, "MM/dd/yyyy") === "01/01/0001"
                            ? <span>Не установленна</span>
                            : <span className='font-normal'>{format(trip.eventDate!, 'MM/dd/yyyy')}</span>
                        }
                    </p>
                    <ScrollArea className="h-[100px] w-full rounded-md border">
                        <div className="p-4">
                            <h4 className="mb-4 text-sm font-medium leading-none">Участники</h4>
                            {trip.tripParticipants?.map((participant) => (
                                <>
                                    <div key={participant.id} className="text-sm">
                                        {UserService.getFullName(participant?.participant!)}
                                    </div>
                                    <Separator className="my-2" />
                                </>
                            ))}
                        </div>
                    </ScrollArea>
                </div>
                {
                    roles.includes("Ranger") &&
                    ( format(trip.eventDate!, "MM/dd/yyyy") === "01/01/0001"
                      ?
                      <div className="w-full">
                        <AddDataEventForm trip={trip}/>
                      </div>
                      : <Button onClick={handlerUpdateStatus}>Пометить как проведенное</Button>
                    )
                }
            </DialogContent>
        </Dialog>
    );
};

export default TripInfoDialog;