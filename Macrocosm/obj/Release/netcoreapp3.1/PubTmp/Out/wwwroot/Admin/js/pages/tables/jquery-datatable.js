//DataTables.js  ����Datatables�����֪ʶ������������������鿴
$.extend($.fn.dataTable.defaults, {
    "dom": "<'row'<'col-sm-12'tr>>" +
        "<'row'<'col-sm-12 text-center'i>>" +
        "<'row'<'col-sm-5'l><'col-sm-7'p>>",//Ĭ����lfrtip
    "processing": true,//������
    "serverSide": true,//������ģʽ����������Ҫ��������Ҫ���ܷ�����ģʽ��
    "searching": false,//datatables�Դ�������
    "scrollX": true,//X������
    "pagingType": "full_numbers",//��ҳģʽ
    "ajax": {
        "type": "GET",//����������Ҫ��
        "contentType": "application/json; charset=utf-8"
    },
    "language": {
        "processing": "������...",
        "lengthMenu": "ÿҳ��ʾ _MENU_ ������",
        "zeroRecords": "û��ƥ����",
        "info": "��ʾ�� _START_ �� _END_ �������� _TOTAL_ ��",
        "infoEmpty": "��ʾ�� 0 �� 0 �������� 0 ��",
        "infoFiltered": "(�� _MAX_ ��������)",
        "infoPostFix": "",
        "search": "����:",
        "url": "",
        "emptyTable": "û��ƥ����",
        "loadingRecords": "������...",
        "thousands": ",",
        "paginate": {
            "first": "��ҳ",
            "previous": "��һҳ",
            "next": "��һҳ",
            "last": "ĩҳ"
        },
        "aria": {
            "sortAscending": ": ���������д���",
            "sortDescending": ": �Խ������д���"
        }
    }
});