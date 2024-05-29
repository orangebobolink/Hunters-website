import { Trip } from '@/entities/trip/models/Trip.ts';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { format } from 'date-fns';
import { Button } from '@/shared/ui';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import { TripService } from '@/entities/trip/api/TripService.ts';
import { TripParticipant } from '@/entities/trip/models/TripParticipant.ts';
import { TripParticipantService } from '@/entities/trip/api/TripParticipantService.ts';
import { toast } from '@/shared/ui/use-toast';
import { ColumnDef } from '@tanstack/react-table';
import { DataTable } from '@/entities/table/ui/data-table';
import TripInfoDialog from '../dialog/trip-info-dialog';

interface IProps {
    trips: Trip[];
    changeRender: boolean;
    setChangeRender: (flag: boolean) => void;
    isOpen: boolean;
    setIsOpen: (flag: boolean) => void;
}

const TripDataTable = ({ trips, changeRender, setChangeRender, isOpen,setIsOpen }: IProps) => {
    const [selectedTrip, setSelectedTrip] = useState<Trip>();
    const { t } = useTranslation('translation', {
        keyPrefix: 'trip.table',
    });
    const { roles, id, huntingLicenseId } = useAppSelector(selectAuth);

    const columns: ColumnDef<Trip>[] = [
        {
            accessorKey: 'permission.land.name',
            header: t('location'),
        },
        {
            accessorKey: 'permission.fromDate',
            header: t('fromDate'),
            cell: ({ row }) => {
                const trip = row.original;

                return (
                    <div className='text-center font-medium'>
                        {format(trip.permission?.fromDate!, 'dd.MM.yyyy')}
                    </div>
                );
            },
        },
        {
            accessorKey: 'permission.toDate',
            header: t('toDate'),
            cell: ({ row }) => {
                const trip = row.original;

                return (
                    <div className='text-center font-medium'>
                        {format(trip.permission?.toDate!, 'dd.MM.yyyy')}
                    </div>
                );
            },
        },
        {
            accessorKey: 'permission.animal.name',
            header: t('animal'),
        },
        {
            accessorKey: 'price',
            header: t('amountOfFee'),
        },
        {
            accessorKey: 'action',
            header: 'Действие',
            cell: ({ row }) => {
                const trip = row.original;

                return (
                    <>
                        {roles.includes('User') &&
                            (!trip.tripParticipants?.some(
                                (p) => p.participantId == id!
                            ) ? (
                                <Button
                                    variant='ghost'
                                    onClick={() => {
                                        handlerPay(trip.number, trip.id!);
                                    }}
                                >
                                    {t('buy')}
                                </Button>
                            ) : (
                                <div>Оплачена</div>
                            ))}

                        {(roles.includes('Manager') ||
                            roles.includes('Ranger')) && (
                            <Button
                                variant='ghost'
                                onClick={() => {
                                    setSelectedTrip(trip);
                                    setIsOpen(true);
                                }}
                            >
                                {t('moreInfo')}
                            </Button>
                        )}
                    </>
                );
            },
        },
    ];

    const handlerPay = async (number: string, tripId: string) => {
        var response = await TripService.payTrip(number);

        if (response.data) {
            console.log(huntingLicenseId);
            const participant: TripParticipant = {
                participantId: id!,
                huntingLicenseId: huntingLicenseId,
                tripId: tripId,
            };

            try {
                const tripResponse = await TripParticipantService.create(
                    participant
                );
                console.log(tripResponse);
                if (tripResponse.status >= 200 && tripResponse.status <= 300) {
                    toast({
                        variant: 'success',
                        title: 'Путевка купленна успешно',
                    });

                    setChangeRender(!changeRender);
                }
            } catch {
                toast({
                    variant: 'destructive',
                    title: 'Что-то пошло не так',
                });
            }
        } else {
        }
    };

    return (
        <div className='w-full'>
            <DataTable columns={columns} data={trips} />
            {selectedTrip && (
                <TripInfoDialog
                    trip={selectedTrip!}
                    isOpen={isOpen}
                    setIsOpen={setIsOpen}
                />
            )}
        </div>
    );
};

export default TripDataTable;
