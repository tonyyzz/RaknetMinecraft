using BaseCommon;
using StressTest.package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StressTest
{
	public class PackageConfig
	{
		/// <summary>
		/// 包体注册
		/// </summary>
		public static void Register()
		{
			#region ------------账号------------
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_login_ret, new PlayerLoginRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_playerInfo_ret, new PlayerInfoRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_playerAttention_ret, new PlayerAttentionRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_playerDetail_ret, new PlayerDetailRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_playerEditSex_ret, new PlayerEditSexRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_playerEditDescription_ret, new PlayerEditDescriptionRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ACCOUNT, (short)SecondCommand.SC_ACCOUNT_playerEditName_ret, new PlayerEditNameRetPacket());
			#endregion

			#region ------------通告------------
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_ANNUNCIATE, (short)SecondCommand.SC_ANNUNCIATE_list_ret, new AnnunciateListRetPacket());
			#endregion

			#region ------------资源------------
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_tagList_ret, new ResourceTagListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_list_ret, new ResourceListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_detail_ret, new ResourceDetailRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_upload_ret, new ResourceUploadRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_download_ret, new ResourceDownloadRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_pointsLike_ret, new ResourcePointsLikeRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_RESOURCE, (short)SecondCommand.SC_RESOURCE_score_ret, new ResourceScoreRetPacket());
			#endregion

			#region ------------评论------------
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_COMMENT, (short)SecondCommand.SC_COMMENT_list_ret, new CommentListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_COMMENT, (short)SecondCommand.SC_COMMENT_operate_ret, new CommentOperateRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_COMMENT, (short)SecondCommand.SC_COMMENT_replyOperate_ret, new CommentReplyOperateRetPacket());
			#endregion

			#region ------------朋友------------
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_list_ret, new FriendListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_searchAdd_ret, new FriendSearchAddListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_requestAdd_ret, new FriendRequestAddRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_requestList_ret, new FriendRequestListRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_requestAgreeOrDisagree_ret, new FriendReqAgreeOrDisagreeRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_detail_ret, new FriendDetailRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_delete_ret, new FriendDeleteRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_recommend_ret, new FriendRecommendRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_FRIEND, (short)SecondCommand.SC_FRIEND_chat_ret, new FriendChatRetPacket());
			#endregion

			#region ------------avatar------------
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_AVATAR, (short)SecondCommand.SC_AVATAR_backpackGoods_ret, new AvatarBackpackGoodsRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_AVATAR, (short)SecondCommand.SC_AVATAR_disguiseSet_ret, new AvatarDisguiseSetRetPacket());
			PackageManage.Instance.RegisterPackage((short)MainCommand.MC_AVATAR, (short)SecondCommand.SC_AVATAR_changeModelSex_ret, new AvatarChangeModelSexPacket());
			#endregion
		}
	}
}
