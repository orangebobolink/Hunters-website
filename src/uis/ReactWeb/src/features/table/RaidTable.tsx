import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {Button} from '@/shared/ui';
import {Feeding} from '@/entities/feeding/Feeding.ts';
import {useState} from 'react';
import FeedingInfoDialog from '@/features/dialog/feeding-info-dialog.tsx';
import {format} from 'date-fns';
import {useTranslation} from 'react-i18next';
import {Raid} from '@/entities/raid/Raid.ts';

interface IProps
{
    raids: Raid[];
}

const RaidTable = ({raids}:IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedRaid, setSelectedRaid] = useState<Raid>();
    const { t} = useTranslation("translation",
        {
            keyPrefix: "raid.table"
        });

    const content = (raid:Raid) => (
        <TableRow key={raid.id}>
            <TableCell>
                {raid.land.name}
            </TableCell>
            <TableCell>
                {format(raid.exitTime, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {format(raid.returnedTime, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {raid.status.toString()}
            </TableCell>
            <TableCell>
                <Button
                    variant="ghost"
                    onClick={() => {
                        setSelectedRaid(raid)
                        setIsOpen(true)
                    }}
                >
                    {t("moreInf")}
                </Button>
            </TableCell>
        </TableRow>
    )

    return (
        <>
            <Table className="justify-center">
                <TableHeader>
                    <TableRow>
                        <TableHead>{t("location")}</TableHead>
                        <TableHead>{t("exitTime")}</TableHead>
                        <TableHead>{t("returnedTime")}</TableHead>
                        <TableHead>{t("status")}</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {raids.map((raid) =>content(raid) )}
                </TableBody>
            </Table>
        </>
    );
};

export default RaidTable;