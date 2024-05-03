import {useEffect, useState} from 'react';
import {useTranslation} from 'react-i18next';
import {Feeding} from '@/entities/feeding/Feeding.ts';
import {FeedingService} from '@/entities/feeding/FeedingService.ts';
import FeedingTable from '@/features/table/FeedingTable.tsx';
import {Button} from '@/shared/ui';
import AddFeedingDialog from '@/features/dialog/add-feeding-dialog.tsx';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';

const FeedingPage = () => {
    const [feedings, setFeedings] = useState<Feeding[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const {roles, id} = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding"
        });

    useEffect(() => {
        const fetchFeedigns = async () => {
            try {
                const response = await FeedingService.getAll();
                if(roles.includes("Ranger"))
                {
                    response.data = response.data.filter((f) => f.receivedId == id)
                }

                setFeedings(response.data);
                console.log(response.data)
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchFeedigns();
    }, [isOpen]);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center space-y-5">
            <div className="w-1/2 flex justify-center">
                <FeedingTable feedings={feedings}/>
            </div>
            <div>
                {
                    roles.includes("Manager") &&
                    <>
                        <Button onClick={()=>{setIsOpen(true)}}>{t("createReport")}</Button>
                        <AddFeedingDialog isOpen={isOpen} setIsOpen={setIsOpen}/>
                    </>
                }
            </div>
        </div>
    );
};

export default FeedingPage;