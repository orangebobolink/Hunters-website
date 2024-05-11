import {Trip} from '@/entities/trip/Trip.ts';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {format} from 'date-fns';
import {Button} from '@/shared/ui';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {TripService} from '@/entities/trip/TripService.ts';
import {TripParticipant} from '@/entities/tripParticipant/TripParticipant.ts';
import {TripParticipantService} from '@/entities/tripParticipant/TripParticipantService.ts';
import TripInfoDialog from '@/features/dialog/trip-info-dialog.tsx';

interface IProps
{
    trips: Trip[];
    setChangeRender: (flag: boolean) => void;
}

const TripTable = ({trips, setChangeRender}:IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedTrip, setSelectedTrip] = useState<Trip>();
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.table"
        });
    const {roles, id, huntingLicenseId} = useAppSelector(selectAuth);

    const handlerPay = async (number:string, tripId:string) => {
        var response = await TripService.payTrip(number)

        if(response.data)
        {
            const participant : TripParticipant = {
                participantId: id!,
                huntingLicenseId: huntingLicenseId,
                tripId: tripId
            }

            const responseParticipant = await TripParticipantService.create(participant);

            setChangeRender(true)
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
                                Данных нет
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