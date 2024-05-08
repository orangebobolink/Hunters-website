import {Trip} from '@/entities/trip/Trip.ts';
import React, {useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {format} from 'date-fns';
import {Button} from '@/shared/ui';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';

interface IProps
{
    trips: Trip[];
}

const TripTable = ({trips}:IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedTrip, setSelectedTrip] = useState<Trip>();
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.table"
        });
    const {roles, id} = useAppSelector(selectAuth);

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
                    <Button
                        variant="ghost"
                        onClick={() => {
                            setSelectedTrip(trip)
                            setIsOpen(true)
                        }}
                    >
                        {t("buy")}
                    </Button>
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
                    {trips?.map((trip) => content(trip) )}
                </TableBody>
            </Table>
        </>
    );
};

export default TripTable;