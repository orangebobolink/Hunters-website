import React, {useState} from 'react';
import {Permission} from '@/entities/permision/Permision.ts';
import {useTranslation} from 'react-i18next';
import {Table, TableBody, TableCell, TableHead, TableHeader, TableRow} from '@/shared/ui/table.tsx';
import {format} from 'date-fns';
import {Button} from '@/shared/ui';
import PermissionInfoDialog from '@/features/dialog/permission-info-dialog.tsx';

interface IProps
{
    permissions: Permission[];
}

const PermissionTable = ({permissions}:IProps) => {
    const [isOpen, setIsOpen] = useState(false);
    const [selectedPermission, setSelectedPermission] = useState<Permission>();
    const { t} = useTranslation("translation",
    {
        keyPrefix: "feeding.table"
    });

    const content = (permission:Permission) => (
        <TableRow key={permission.id}>
            <TableCell>
                {permission.number}
            </TableCell>
            <TableCell>
                {format(permission.fromDate, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {format(permission.toDate, "dd.MM.yyyy")}
            </TableCell>
            <TableCell>
                {permission.animal?.name}
            </TableCell>
            <TableCell>
                {permission.land.name}
            </TableCell>
            <TableCell>
                {permission.numberOfCoupons}
            </TableCell>
            <TableCell>
                {permission.status.toString()}
            </TableCell>
            <TableCell>
                <Button
                    variant="ghost"
                    onClick={() => {
                        setSelectedPermission(permission)
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
                        <TableHead>{t("fromDate")}</TableHead>
                        <TableHead>{t("toDate")}</TableHead>
                        <TableHead>{t("animalName")}</TableHead>
                        <TableHead>{t("location")}</TableHead>
                        <TableHead>{t("couponLength")}</TableHead>
                        <TableHead>{t("status")}</TableHead>
                        <TableHead> </TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {permissions?.map((feeding) => content(feeding) )}
                </TableBody>
            </Table>
            {selectedPermission && <PermissionInfoDialog permission={selectedPermission} isOpen={isOpen} setIsOpen={setIsOpen}/>}
        </>
    );
};

export default PermissionTable;