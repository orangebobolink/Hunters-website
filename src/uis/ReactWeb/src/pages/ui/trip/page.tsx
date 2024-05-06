import {useEffect, useState} from 'react';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {useTranslation} from 'react-i18next';
import {Trip} from '@/entities/trip/Trip.ts';
import {TripService} from '@/entities/trip/TripService.ts';
import {Dialog, DialogContent, DialogTrigger} from '@/shared/ui/dialog.tsx';
import {Button} from '@/shared/ui';
import TripTable from '@/features/table/trip-table.tsx';

const TripPage = () => {
    const [trips, setTrips] = useState<Trip[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const {roles, id} = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding"
        });

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await TripService.getAll();

                if(roles.includes("Ranger"))
                {
                    response.data = response.data.filter((f) => f.receivedId == id)
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
                <TripTable trips={trips}/>
            </div>
            {roles.includes("Manager") &&
                <Dialog onOpenChange={() => setIsOpen(false)}>
                    <DialogTrigger asChild>
                        <Button onClick={() => setIsOpen(true)}
                                variant="outline">Добавить путевку</Button>
                    </DialogTrigger>
                    <DialogContent>
                       
                    </DialogContent>
                </Dialog>
            }
        </div>
    );
};

export default TripPage;