import {useEffect, useState} from 'react';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {useTranslation} from 'react-i18next';
import {Trip} from '@/entities/trip/models/Trip.ts';
import {TripService} from '@/entities/trip/api/TripService.ts';
import {Dialog, DialogContent, DialogTrigger} from '@/shared/ui/dialog.tsx';
import {Button} from '@/shared/ui';
import TripTable from '@/features/table/trip-table.tsx';
import CreateTripForm from '@/entities/trip/ui/create-trip-form.tsx';
import {toast} from '@/shared/ui/use-toast.ts';
import {Status} from '@/entities/status/Status.ts';

const TripPage = () => {
    const [trips, setTrips] = useState<Trip[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const [changeRender, setChangeRender] = useState(false);
    const {roles, id, isPaid} = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding"
        });

    useEffect(() => {
        if(roles.includes("User") && !isPaid)
        {
            toast({
                variant: "destructive",
                title: "Требуется оплатить пошлины",
                description: "Перейдите в раздел 'Оплата пошлин' и оплатите пошлины по вашей охотничьей лицензии"
            })
        }
    }, [isPaid]);

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await TripService.getAll();

                if(roles.includes("Ranger"))
                {
                    console.log(response.data)
                    console.log(id)
                    response.data = response.data.filter((f) => f.permission?.receivedId == id
                        && f.status != Status[Status.Completed])
                }

                setTrips(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [isOpen]);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center space-y-5">
            <div className="w-2/3 flex justify-center">
                <TripTable trips={trips} setChangeRender={setChangeRender}/>
            </div>
            {roles.includes("Manager") &&
                <Dialog onOpenChange={() => setIsOpen(false)}>
                    <DialogTrigger asChild>
                        <Button onClick={() => setIsOpen(true)}
                                variant="outline">Добавить путевку</Button>
                    </DialogTrigger>
                    <DialogContent>
                       <CreateTripForm/>
                    </DialogContent>
                </Dialog>
            }
        </div>
    );
};

export default TripPage;