import React, {useEffect, useState} from 'react';
import {Trip} from '@/entities/trip/Trip.ts';
import {TripService} from '@/entities/trip/TripService.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import TripTable from '@/features/table/trip-table.tsx';

const MyTripsPage = () => {
    const [trips, setTrips] = useState<Trip[]>([])
    const {id} = useAppSelector(selectAuth);

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await TripService.getByParticipantId(id);
                setTrips(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [id]);

    return (
        <div>
           <TripTable trips={trips} setChangeRender={(d:boolean)=>{}}/>
        </div>
    );
};

export default MyTripsPage;