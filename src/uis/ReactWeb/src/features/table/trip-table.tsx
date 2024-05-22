import {Trip} from '@/entities/trip/models/Trip.ts';
import {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {format} from 'date-fns';
import {Button} from '@/shared/ui';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {TripService} from '@/entities/trip/api/TripService.ts';
import {TripParticipant} from '@/entities/trip/models/TripParticipant.ts';
import {TripParticipantService} from '@/entities/trip/api/TripParticipantService.ts';
import TripInfoDialog from '@/features/dialog/trip-info-dialog.tsx';
import { toast } from '@/shared/ui/use-toast';

interface IProps
{
    trips: Trip[];
    changeRender:boolean;
    setChangeRender: (flag: boolean) => void;
}

const TripTable = ({trips, changeRender, setChangeRender}:IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedTrip, setSelectedTrip] = useState<Trip>();
    const { t} = useTranslation("translation",
        {
            keyPrefix: "trip.table"
        });
    const {roles, id, huntingLicenseId} = useAppSelector(selectAuth);

    const handlerPay = async (number:string, tripId:string) => {
        var response = await TripService.payTrip(number)

        if(response.data)
        {
            console.log(huntingLicenseId)
            const participant : TripParticipant = {
                participantId: id!,
                huntingLicenseId: huntingLicenseId,
                tripId: tripId
            }

            try {
            const tripResponse = await TripParticipantService.create(participant);

                if(tripResponse.data.status >= 200 && tripResponse.data.status <= 300)
                {
                    toast({
                        variant: "success",
                        title: "Путевка купленна успешно",
                    })

                    setChangeRender(!changeRender)
                }
            }catch {
                toast({
                    variant: "destructive",
                    title: "Что-то пошло не так",
                })
            }
        }
        else
        {

        }
    }

    const content = (trip:Trip) => (
        <TableRow key={trip.id}>
            <TableCell>
                {trip.permission?.land.name}
            </TableCell>
            <TableCell>
                {format(trip.permission?.fromDate!, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {format(trip.permission?.toDate!, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {trip.permission?.animal?.name}
            </TableCell>
            <TableCell>
                {trip.price}
            </TableCell>
            <TableCell>
                {roles.includes("User") &&
                    (!trip.tripParticipants?.some(p => p.participantId == id!)
                    ?
                        <Button
                            variant="ghost"
                            onClick={()=>{handlerPay(trip.number, trip.id!)}}
                        >
                            {t("buy")}
                        </Button>
                    :
                    <div>Оплачена</div>)
                }

                {
                    (roles.includes("Manager") || roles.includes("Ranger")) &&
                    <Button
                        variant="ghost"
                        onClick={() => {
                            setSelectedTrip(trip)
                            setIsOpen(true)
                        }}
                    >
                        {t("moreInfo")}
                    </Button>
                }
            </TableCell>
        </TableRow>
    )

    return (
        <>
            <Table className="justify-center">
                <TableHeader>
                    <TableRow>
                        <TableHead>{t("location")}</TableHead>
                        <TableHead>{t("fromDate")}</TableHead>
                        <TableHead>{t("toDate")}</TableHead>
                        <TableHead>{t("animal")}</TableHead>
                        <TableHead>{t("amountOfFee")}</TableHead>
                        <TableHead> </TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {
                        trips.length == 0 ?
                        <TableRow>
                            <TableCell colSpan={6} className="text-center">
                                {t("emptyTable")}
                            </TableCell>
                        </TableRow>
                        :
                        trips?.map((trip) => content(trip) )
                    }
                </TableBody>
            </Table>
            {selectedTrip && <TripInfoDialog trip={selectedTrip!} isOpen={isOpen} setIsOpen={setIsOpen}/>}
        </>
    );
};

export default TripTable;