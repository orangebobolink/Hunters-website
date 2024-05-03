import {useEffect, useState} from 'react';
import {Raid} from '@/entities/raid/Raid.ts';
import {useAppSelector} from '@/shared/lib/hooks/redux-hooks.ts';
import {selectAuth} from '@/shared/model/store/selectors/auth.selectors.ts';
import {RaidService} from '@/entities/raid/RaidService.ts';
import {Button} from '@/shared/ui';
import {useTranslation} from 'react-i18next';
import RaidTable from '@/features/table/RaidTable.tsx';
import AddRaidDialog from '@/features/dialog/add-raid-dialog.tsx';

const RaidPage = () => {
    const [raids, setRaids] = useState<Raid[]>([])
    const [isOpen, setIsOpen] = useState(false);
    const {id} = useAppSelector(selectAuth);
    const { t} = useTranslation("translation",
        {
            keyPrefix: "raid"
        });

    useEffect(() => {
        const fetchFeedigns = async () => {
            try {
                const response = await RaidService.getRaidsById(id!);

                setRaids(response.data);
            } catch (error) {
                console.error('Error fetching users:', error);
            }
        };

        fetchFeedigns();
    }, [isOpen]);

    return (
        <div className="select-none h-full w-full flex items-center flex-col justify-center space-y-5">
            <div className="w-1/2 flex justify-center">
                <RaidTable raids={raids}/>
            </div>
            <div>
                {
                    <>
                        <Button onClick={()=>{setIsOpen(true)}}>{t("createReport")}</Button>
                        <AddRaidDialog isOpen={isOpen} setIsOpen={setIsOpen}/>
                    </>
                }
            </div>
        </div>
    );
};

export default RaidPage;