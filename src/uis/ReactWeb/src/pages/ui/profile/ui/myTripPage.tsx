import { useEffect, useState } from 'react';
import { Trip } from '@/entities/trip/models/Trip.ts';
import { TripService } from '@/entities/trip/api/TripService.ts';
import { useAppSelector } from '@/shared/lib/hooks/redux-hooks.ts';
import { selectAuth } from '@/shared/model/store/selectors/auth.selectors.ts';
import TripDataTable from '@/features/table/trip-data-table';

const MyTripsPage = () => {
    const [trips, setTrips] = useState<Trip[]>([]);
    const { id } = useAppSelector(selectAuth);

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await TripService.getByParticipantId(id);
                setTrips(response.data);
                console.log(response.data);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [id]);

    return (
        <div>
            <TripDataTable trips={trips} setChangeRender={(d: boolean) => {}} />
        </div>
    );
};

export default MyTripsPage;
