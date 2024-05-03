import {Feeding} from '@/entities/feeding/Feeding.ts';
import {Dialog, DialogContent} from '@/shared/ui/dialog.tsx';
import {useEffect, useState} from 'react';
import {UserService} from '@/entities/user/UserService.ts';
import {User} from '@/entities/user/User.ts';
import {Button} from '@/shared/ui';
import {FeedingService} from '@/entities/feeding/FeedingService.ts';
import {toast} from '@/shared/ui/use-toast.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {Status} from '@/entities/status/Status.ts';
import {AxiosResponse} from 'axios';
import {format} from 'date-fns';

interface IProps {
    feeding: Feeding,
    isOpen:boolean,
    setIsOpen:(flag:boolean)=>void
}

const FeedingInfoDialog = ({feeding, isOpen, setIsOpen}:IProps) => {
    const [issued, setIssued] = useState<User>()
    const [recieved, setRecieved] = useState<User>()
    const {roles} = useAppSelector(selectAuth);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const recieved = await UserService.getById(feeding.receivedId);
                setRecieved(recieved.data)
                const issued = await UserService.getById(feeding.issuedId);
                setIssued(issued.data);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchUsers();
    }, []);

    const handlerDelete = async () => {
        const response = await FeedingService.delete(feeding.id!)

        if(response.status >= 200 && response.status <= 300)
        {
            toast({
                variant: "success",
                title: "Подкормка удаленна успешно",
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

    const handlerReceived = async () => {
        feeding.status = Status[Status.Recived]
        feeding.receivedDate = new Date()
        const response = await FeedingService.update(feeding)

        handler(response)
    }

    const handlerComplited = async () => {
        feeding.status = Status[Status.Compiled];
        const response = await FeedingService.update(feeding)

        handler(response)
    }

    return (
        <Dialog open={isOpen} onOpenChange={()=>setIsOpen(false)}>
            <DialogContent>
                <div className="p-4 shadow-md rounded-lg">
                    <p className="font-bold">Номер: <span className="font-normal">{feeding.number}</span></p>
                    <p className="font-bold">Дата подкормки: <span className="font-normal">{format(feeding.feedingDate, "MM/dd/yyyy")}</span></p>
                    <p className="font-bold">Составитель: <span className="font-normal">{UserService.getFullName(issued)}</span></p>
                    <p className="font-bold">Выданно егерю: <span className="font-normal">{UserService.getFullName(recieved)}</span></p>
                    <p className="font-bold">Дата получения: <span className="font-normal">{feeding.receivedDate ? format(feeding.receivedDate, "MM/dd/yyyy") : "Not Received"}</span></p>
                    <p className="font-bold">Состояние: <span className="font-normal">{feeding.status}</span></p>
                    <p className="font-bold">Локация: <span className="font-normal">{feeding.land.name}</span></p>
                    <h4 className="font-semibold text-lg mt-4">Продукты подкормки:</h4>
                    <ul className="list-disc pl-5">
                        {feeding.products.map((product, index) => (
                            <li key={index} className="font-normal">
                                {product.product}: {product.quantity} {product.unitOfMeasurement}
                            </li>
                        ))}
                    </ul>
                </div>
                {
                    roles.includes("Manager") &&  <Button onClick={handlerDelete}>Удалить</Button>
                }
                {
                    (roles.includes("Ranger") && feeding.status == Status[Status.Given]) &&
                        <Button onClick={handlerReceived}>Приянть</Button>
                }
                {
                    (roles.includes("Ranger") && feeding.status == Status[Status.Recived]) &&
                    <Button onClick={handlerComplited}>Пометить как выполненное</Button>
                }
            </DialogContent>
        </Dialog>
    );
};

export default FeedingInfoDialog;