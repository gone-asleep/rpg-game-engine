using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {
    public interface INetworkAdapter {
        void Send(ActionParameters parameter);
        ActionParameters[] Recieve();
    }

    public class LoopbackNetworkAdapter : INetworkAdapter {
        List<ActionParameters> pretendServer = new List<ActionParameters>();
       
        public void Send(ActionParameters parameter) {
            pretendServer.Add(parameter);
        }
        public ActionParameters[] Recieve() {
            var list = pretendServer.ToArray();
            pretendServer.Clear();
            return list;
        }
    }
}
