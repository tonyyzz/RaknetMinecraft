using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	class ThreadPoolReq
	{
		public static void Start()
		{
			int loginReqCount = 1; //登录请求数量
			int otherReqCount = 1; //其他接口请求数量（使用的是同一个账号）
			ThreadPoolPlayerLogin.Start(loginReqCount,
				beforeAction: null,
				afterAction: () =>
				{
					#region ------------账号------------
					//ThreadPoolPlayerInfo.Start(reqCount: otherReqCount);
					//ThreadPoolPlayerAttention.Start(reqCount: otherReqCount, beforeAction: () => { BLL.BaseBLL.TruncateTable(new Model.PlayerAttentionModel()); });
					//ThreadPoolPlayerDetail.Start(reqCount: otherReqCount);
					//ThreadPoolPlayerEditSex.Start(reqCount: otherReqCount);
					//ThreadPoolPlayerEditDescription.Start(reqCount: otherReqCount);
					//ThreadPoolPlayerEditName.Start(reqCount: otherReqCount);
					#endregion

					#region ------------通告------------
					//ThreadPoolAnnunciateList.Start(reqCount: otherReqCount);
					#endregion

					#region ------------资源------------
					//ThreadPoolResourceTagList.Start(reqCount: otherReqCount);
					//ThreadPoolResourceList.Start(reqCount: otherReqCount);
					//ThreadPoolResourceDetail.Start(reqCount: otherReqCount);
					//ThreadPoolResourceUpload.Start(reqCount: otherReqCount);
					//ThreadPoolResourceDownload.Start(reqCount: otherReqCount);
					//ThreadPoolResourcePointsLike.Start(reqCount: otherReqCount);
					//ThreadPoolResourceScore.Start(reqCount: otherReqCount);
					#endregion

					#region ------------评论------------
					//ThreadPoolCommentList.Start(reqCount: otherReqCount);
					//ThreadPoolCommentOperate.Start(reqCount: otherReqCount);
					//ThreadPoolCommentReplyOperate.Start(reqCount: otherReqCount);
					#endregion

					#region ------------朋友------------
					//ThreadPoolFriendList.Start(reqCount: otherReqCount);
					//ThreadPoolFriendSearchAdd.Start(reqCount: otherReqCount);
					//ThreadPoolFriendRequestAdd.Start(reqCount: otherReqCount, beforeAction: () => { BLL.BaseBLL.TruncateTable(new Model.FriendRequestModel()); });
					//ThreadPoolFriendRequestList.Start(reqCount: otherReqCount, beforeAction: () => { InitFriendReqTable(loginReqCount); });
					//ThreadPoolFriendReqAgreeOrDisagree.Start(reqCount: otherReqCount, beforeAction: () => { InitFriendReqTable(loginReqCount, otherReqCount); BLL.BaseBLL.TruncateTable(new Model.FriendModel()); });
					//ThreadPoolFriendDetail.Start(reqCount: otherReqCount);
					//ThreadPoolFriendDelete.Start(reqCount: otherReqCount, beforeAction: () => { InitFriendTavle(loginReqCount, otherReqCount); });

					//较慢
					//ThreadPoolFriendRecommend.Start(reqCount: otherReqCount);

					//ThreadPoolFriendChat.Start(reqCount: otherReqCount);
					#endregion

					#region ------------avatar------------
					//ThreadPoolAvatarBackpackGoods.Start(reqCount: otherReqCount);
					//ThreadPoolAvatarDisguiseSet.Start(reqCount: otherReqCount);
					ThreadPoolAvatarChangeModelSex.Start(reqCount: otherReqCount);
					#endregion
				});
		}

		/// <summary>
		/// 初始化好友申请表
		/// </summary>
		static void InitFriendReqTable(int playerId, int friendCount = 10)
		{
			BLL.BaseBLL.TruncateTable(new Model.FriendRequestModel());
			List<Model.FriendRequestModel> list = new List<Model.FriendRequestModel>();
			for (int i = 1; i <= friendCount + 1; i++)
			{
				if (i == playerId)
				{
					continue;
				}
				Model.FriendRequestModel model = new Model.FriendRequestModel
				{
					PlayerId = i,
					FriendId = playerId,
					RequestTime = DateTime.Now,
					RequestState = 0
				};
				list.Add(model);
			}
			BLL.BaseBLL.BatchInsert(list);
		}

		/// <summary>
		/// 初始化好友表
		/// </summary>
		/// <param name="playerId"></param>
		/// <param name="friendCount"></param>
		static void InitFriendTavle(int playerId, int friendCount)
		{
			BLL.BaseBLL.TruncateTable(new Model.FriendModel());
			List<Model.FriendModel> list = new List<Model.FriendModel>();
			for (int i = 1; i <= friendCount + 1; i++)
			{
				if (i == playerId)
				{
					continue;
				}
				Model.FriendModel model = new Model.FriendModel
				{
					PlayerId = i,
					FriendId = playerId,
					AddTime = DateTime.Now
				};
				list.Add(model);
			}
			BLL.BaseBLL.BatchInsert(list);
		}
	}
}
