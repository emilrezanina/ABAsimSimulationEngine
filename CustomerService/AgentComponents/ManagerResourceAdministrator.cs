﻿using System;
using SimulationEngine.Communication;
using SimulationEngine.Modules.SimulationModelModule.Components;

namespace CustomerService.AgentComponents
{
    class ManagerResourceAdministrator : ControlManager
    {
        public ManagerResourceAdministrator(string name)
            : base(name)
        {
        }

        public override void ProcessTheMessage(Message message)
        {
            switch (message.Type)
            {
                case TypeMessage.Request:
                    ProcessRequestMessage(message);
                    break;
                case TypeMessage.Finish:
                    ProcessFinishMessage(message);
                    break;
                case TypeMessage.Notice:
                    ProcessNoticeMessage(message);
                    break;
                default:
                    throw new Exception(message.ToString());
            }
        }

        

        private void ProcessNoticeMessage(Message message)
        {
            switch (message.Code)
            {
                case MessageCodeManager.ReturnResource:
                    //p6 - uvolneni zdroje
                    var msg = MessageProvider.CreateMessage(TypeMessage.Execute, Name, ComponentNameManager.ActionReturnResource, 
                        MessageCodeManager.ReturnResource, message.DataParameters, message.Timestamp);
                    SendExecuteMessage(msg);
                    //p9 - je fronta na vraceny zdroj
                    msg = MessageProvider.CreateMessage(TypeMessage.Execute, Name, ComponentNameManager.QueryIsQueueOfApplicantEmpty,
                        MessageCodeManager.IsQueueOfApplicantEmpty, msg.DataParameters, message.Timestamp);
                    SendExecuteMessage(msg);
                    //fronta neni prazdna
                    if (msg.Result.Equals(ResultNameManager.QueueIsntEmpty))
                    {
                        //DODELAT PARAMETR RIKAJICI O JAKOU FRONTU SE JEDNA
                        msg = MessageProvider.CreateMessage(TypeMessage.Execute, Name, ComponentNameManager.ActionRemoveApplicantFromQueue, null,
                            null, message.Timestamp);
                        msg.AddDataParameter(ParameterNameManager.Resource, message.DataParameters[ParameterNameManager.Resource]);
                        SendExecuteMessage(msg);
                        msg.DeleteDataParameter(ParameterNameManager.Resource);
                        //p8 - Prideleni zdroje zakaznikovi
                        msg.Addressee = ComponentNameManager.ActionAssignResource;
                        SendExecuteMessage(msg);
                        //p10 - Premistit zdroj
                        msg.Addressee = ComponentNameManager.QueryNeedMoveResource;
                        SendExecuteMessage(msg);
                        //zdroj premistit
                        if (msg.Result.Equals(MessageCodeManager.MoveResource))
                        {
                            msg = MessageProvider.CreateMessage(TypeMessage.Start, Name, ComponentNameManager.ProcessMoveResource, null,
                                msg.DataParameters, message.Timestamp);
                            SendStartMessage(msg);
                        } //zdroj nepremistnit
                        else
                        {
                            msg = MessageProvider.CreateMessage(TypeMessage.Response, Name,
                                ComponentNameManager.AgentService, MessageCodeManager.DeliverResource,
                                msg.DataParameters, message.Timestamp);
                            msg.Answer = message.Answer;
                            SendResponseMessage(msg);
                        }
                    }
                    //fronta je prazdna
                    break;
            }
        }

        private void ProcessFinishMessage(Message message)
        {
            switch (message.Code)
            {
                case MessageCodeManager.CompleteMoveResource:
                    var msg = MessageProvider.CreateMessage(TypeMessage.Response, Name,
                        ComponentNameManager.AgentService,
                        MessageCodeManager.DeliverResource, message.DataParameters, message.Timestamp);
                    msg.Answer = message.Answer;
                    SendResponseMessage(msg);
                    break;
            }
        }

        private void ProcessRequestMessage(Message message)
        {
            var msg = MessageProvider.CreateMessage(TypeMessage.Execute, Name, ComponentNameManager.AdvisorSelectionOfFreeResources, null,
                message.DataParameters, message.Timestamp);
            SendExecuteMessage(msg);
            //pridelit zdroj
            if (msg.Result.Equals(ResultNameManager.AssignResource))
            {
                //p8 - Prideleni zdroje zakaznikovi
                msg.Addressee = ComponentNameManager.ActionAssignResource;
                SendExecuteMessage(msg);
                //p10 - Premistit zdroj
                msg.Addressee = ComponentNameManager.QueryNeedMoveResource;
                SendExecuteMessage(msg);
                //zdroj premistit
                if (msg.Result.Equals(MessageCodeManager.MoveResource))
                {
                    msg = MessageProvider.CreateMessage(TypeMessage.Start, Name, ComponentNameManager.ProcessMoveResource,
                        null, msg.DataParameters, message.Timestamp);
                    SendStartMessage(msg);
                } //zdroj nepremistnit
                else
                {
                    msg = MessageProvider.CreateMessage(TypeMessage.Response, Name, ComponentNameManager.AgentService,
                        message.Code, msg.DataParameters, message.Timestamp);
                    msg.Answer = message.Answer;
                    SendResponseMessage(msg);
                }
            } //nepridelit zdroj
            else
            {
                //provedeni premisteni z cekani na obsluhu na zadatelObsluhaA
                msg.Addressee = ComponentNameManager.ActionPutApplicantToQueueOnResource;
                msg.Sender = Name;
                SendExecuteMessage(msg);
            }
        }
    }
}
