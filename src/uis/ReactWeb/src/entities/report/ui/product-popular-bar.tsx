import {Bar, BarChart, CartesianGrid, Legend, Rectangle, ResponsiveContainer, Tooltip, XAxis, YAxis} from 'recharts';
import {useEffect, useState} from 'react';
import {ReportService} from '@/entities/report/api/ReportService.ts';
import {Period} from '@/entities/report/models/Period.ts';
import {useTheme} from '@/shared/lib/hooks/useTheme.ts';
import {LoadingSpinner} from '@/shared/ui/loading-spinner.tsx';
import PeriodSelect from '@/entities/report/ui/period-select.tsx';

const CustomTooltip = ({ active, payload, label }: any) => {
    console.log(payload)
    if (active && payload && payload.length) {
        return (
            <div className="custom-tooltip font-bold text-pink-120">
                <p className="label">{`Название ${label} : ${payload[0].payload?.price}`}</p>
                <p className="label">{`Цена ${payload[0].payload?.price}`}</p>
                <p className="label">{`Количество купивших ${payload[0].value}`}</p>
            </div>
        );
    }

    return null;
};

const ProductPopularBar = () => {
    const [productsPopular, setProductsPopular] = useState<ProductPopular[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const {theme } = useTheme()
    const [period, setPeriod] = useState<Period>(Period.Ever)

    useEffect(() => {
        const fetchPermissions = async () => {
            try {
                const response = await ReportService.getProductsByPopular(period);

                setProductsPopular(response.data);
                setIsLoading(false)
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchPermissions();
    }, [period]);

    return (
        <div className=' flex flex-col justify-center items-center mt-4 min-w-[350px] w-[20dvw] h-[250px]'>
            <PeriodSelect period={period}
                          setPeriod={setPeriod}/>
            <ResponsiveContainer width='100%'
                                 height='100%'>
                {isLoading
                 ? (
                     <LoadingSpinner className='w-1/2'/>
                 )
                 : (
                     <BarChart data={productsPopular}>
                         <CartesianGrid strokeDasharray='3 3'/>
                         <XAxis dataKey='name'/>
                         <YAxis/>
                         <Tooltip content={<CustomTooltip/>}/>
                         <Bar
                             dataKey='rentedQuantity'
                             fill='#8884d8'
                             activeBar={<Rectangle fill='pink'
                                                   stroke='blue'/>}
                         />
                         <Legend/>
                     </BarChart>
                 )}
            </ResponsiveContainer>
        </div>
    );
};

export default ProductPopularBar;
