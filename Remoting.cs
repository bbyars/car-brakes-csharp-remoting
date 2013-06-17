// Remoting dll code, program 2
// Ryan Rozelle - CS553 Fall 2012

using System;

namespace Remote
{
	[Serializable]
	public struct kAction
	{
        public int frictionL;
        public int frictionR;
        public int tempL;
        public int tempR;
        public int interiorTemp;
        public int brake;
	};

	[Serializable]
	public struct kResponse
	{
		public string    s_Result;   // the response text sent by Server
	};

	public class cTransfer : MarshalByRefObject
	{
		public delegate kResponse      del_ServerCall(kAction k_Action);
		public event    del_ServerCall  ev_ServerCall;

		// Default public no argument constructor
		public cTransfer() 
		{
		}

		public kResponse CallServer(kAction k_Action)
		{
			return ev_ServerCall(k_Action);			
		}

		/// <summary>
		/// This override is EXTREMELY important
		/// If it is missing the garbage collector of the Server will delete the cTransfer object
		/// after 5 minutes and the event will be lost, so further calls to the Server will return
		/// "Server encountered an internal error" (a very helpful Mircrosoft error message!)
		/// This function sets the livetime to infinite
		/// See http://www.thinktecture.com/Resources/RemotingFAQ/SINGLETON_IS_DYING.html
		/// </summary>
		public override Object InitializeLifetimeService()
		{
			return null;
		}
	}
}

