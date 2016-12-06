<%@ Page Title="" Language="C#" MasterPageFile="~/production/MasterPage.master" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="production_category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="x_content">

        <p>Quản lý danh sách danh mục</p>

        <div class="table-responsive">
            <%if (cate != null)
              { %>
            <table class="table table-striped jambo_table bulk_action">
                <thead>
                    <tr class="headings">

                        <th class="column-title">ID </th>
                        <th class="column-title">Name </th>
                        <th class="column-title">Description </th>
                        <th class="column-title">Sort order </th>
                        <th class="column-title no-link last"><span class="nobr">Action</span>
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <%for (int i = 0; i < cate.Count; i++)
                      { %>
                    <tr class="even pointer">

                        <td class=" "><%=cate[i].category_id %></td>
                        <td class=" "><%=cate[i].category_name %> </td>
                        <td class=" "><%=cate[i].category_description %> </td>
                        <td class=" "><%=cate[i].sort_order %><i class="success fa fa-long-arrow-up"></i></td>

                        <td class=" last"><a href="#">View</a>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
            <%} %>
        </div>
    </div>
    <div id="myModal" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                    <h4 class="modal-title" id="myModalLabel2">Chỉnh sửa danh mục</h4>
                </div>
                <div class="modal-body">
                    <form id="antoform" class="form-horizontal calender" role="form">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Name</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="name" name="name">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Description</label>
                            <div class="col-sm-9">
                                <textarea class="form-control" style="height: 55px;" id="descr" name="descr"></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Sort order
                            </label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control" id="sort" name="title">
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>

            </div>
        </div>
    </div>

</asp:Content>

