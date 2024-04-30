import {useEffect, useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Feeding} from '@/entities/feeding/Feeding.ts';
import {FeedingService} from '@/entities/feeding/FeedingService.ts';
import FeedingTable from '@/features/table/FeedingTable.tsx';
import {Button} from '@/shared/ui';
import {RangerCombobox} from '@/features/combobox/ranger-combobox.tsx';
import {LandCombobox} from '@/features/combobox/land-combobox.tsx';

const FeedingPage = () => {

    const [feedings, setFeedings] = useState<Feeding[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "animal"
        });

    useEffect(() => {
        const fetchFeedigns = async () => {
            try {
                const response = await FeedingService.getAll();
                setFeedings(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchFeedigns();
    }, []);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center">
            <div className="w-1/2 flex justify-center">
                <FeedingTable feedings={feedings}/>
            </div>
            <Button>Создать отчет</Button>
        </div>
    );
};

export default FeedingPage;