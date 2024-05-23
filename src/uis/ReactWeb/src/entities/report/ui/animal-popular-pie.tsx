import { useEffect, useState } from 'react';
import { Period } from '../models/Period';
import { Pie, PieChart, ResponsiveContainer, Tooltip } from 'recharts';
import PeriodSelect from './period-select';
import { LoadingSpinner } from '@/shared/ui/loading-spinner';
import { AnimalPopular } from '../models/AnimalPopular';
import { ReportService } from '../api/ReportService';

const AnimalPopularPie = () => {
    const [animalsPopular, setAnimalsPopular] = useState<AnimalPopular[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [period, setPeriod] = useState<Period>(Period.Ever);

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await ReportService.getAnimalsByPopular(
                    period
                );

                setAnimalsPopular(response.data);
                setIsLoading(false);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [period]);

    return (
        <div className=' flex flex-col justify-center items-center mt-4 min-w-[350px] w-full h-[250px]'>
            <PeriodSelect period={period} setPeriod={setPeriod} />
            <ResponsiveContainer width='100%' height='100%'>
                {isLoading ? (
                    <LoadingSpinner className='w-1/2' />
                ) : (
                    <PieChart width={400} height={400}>
                        <Pie
                            dataKey='quantity'
                            isAnimationActive={false}
                            data={animalsPopular}
                            cx='50%'
                            cy='50%'
                            outerRadius={80}
                            fill='#8884d8'
                            label
                        />

                        <Tooltip />
                    </PieChart>
                )}
            </ResponsiveContainer>
        </div>
    );
};

export default AnimalPopularPie;
