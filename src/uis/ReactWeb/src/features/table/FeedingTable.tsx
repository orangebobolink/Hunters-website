import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {Button} from '@/shared/ui';
import {Feeding} from '@/entities/feeding/Feeding.ts';
import {useState} from 'react';
import FeedingInfoDialog from '@/features/dialog/feeding-info-dialog.tsx';
import {format} from 'date-fns';
import {useTranslation} from 'react-i18next';

interface IProps
{
    feedings: Feeding[];
}

const FeedingTable = ({feedings}:IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedFeeding, setSelectedFeeding] = useState<Feeding>();
    const { t} = useTranslation("translation",
        {
            keyPrefix: "feeding.table"
        });

    const content = (feeding:Feeding) => (
        <TableRow key={feeding.id}>
            <TableCell>
                {feeding.number}
            </TableCell>
            <TableCell>
                {format(feeding.feedingDate, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {feeding.land.name}
            </TableCell>
            <TableCell>
                {feeding.status.toString()}
            </TableCell>
            <TableCell>
                <Button
                    variant="ghost"
                    onClick={() => {
                        setSelectedFeeding(feeding)
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
                    <TableHead>{t("number")}</TableHead>
                    <TableHead>{t("feedingDate")}</TableHead>
                    <TableHead>{t("location")}</TableHead>
                    <TableHead>{t("status")}</TableHead>
                    <TableHead>{t("act")}</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {feedings.map((feeding) =>content(feeding) )}
            </TableBody>
        </Table>
            {selectedFeeding && <FeedingInfoDialog feeding={selectedFeeding} isOpen={isOpen} setIsOpen={setIsOpen}/>}
        </>
    );
};

export default FeedingTable;