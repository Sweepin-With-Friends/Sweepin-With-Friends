using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;



    public class NetStartGame : NetMessage
    {

        public int AssignedTeam { get; set; }

        public NetStartGame()
        {

            Code = OpCode.WELCOME;

        }

        public NetStartGame(DataStreamReader reader)
        {

            Code = OpCode.WELCOME;
            Deserialize(reader);

        }


        public override void Serialize(ref DataStreamWriter writer)
        {
            writer.WriteByte((byte)Code);
            writer.WriteInt(AssignedTeam);

        }

        public override void Deserialize(DataStreamReader reader)
        {
            AssignedTeam = reader.ReadInt();
        }

        public override void ReceivedOnClient()
        {
            NetUtility.C_START_GAME?.Invoke(this);
        }

        public override void ReceivedOnServer(NetworkConnection cnn)
        {
            NetUtility.S_START_GAME?.Invoke(this, cnn);
        }



    }

