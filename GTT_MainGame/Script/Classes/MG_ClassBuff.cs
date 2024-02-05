using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassBuff {
	public string type;
	public int ownerID, turnDuration, buffID;

	public bool stackable, endTurnDurEffect;
	public int stack = 1, stackMax, stackStart;

	// Attachment
	public bool attachment_has = false;
	public GameObject attachment_obj;
	public float attach_offsetX, attach_offsetY;

	public MG_ClassBuff(string newType, int newOwnerId, int newBuffId){
		type = newType;
		ownerID = newOwnerId;
		buffID = newBuffId;

		MG_DB_Buffs.I._setData (newType);
		turnDuration 					= MG_DB_Buffs.I.duration;
		stackable 						= MG_DB_Buffs.I.stackable;
		stackStart 						= MG_DB_Buffs.I.stackStart;
		stackMax 						= MG_DB_Buffs.I.stackMax;
		endTurnDurEffect 				= MG_DB_Buffs.I.endTurnDurEffect;

		stack = stackStart;

		// Attachment
		attachment_has = MG_DB_Attachments.I.hasAttachment(type);
		if (attachment_has && MG_GetUnit.I._checkIfUnitWithIDExists(ownerID)) {
			attachment_obj = MG_DB_Attachments.I._getSprite (type);

			MG_DB_Attachments.I._setAttachmentData (type);
			attach_offsetX = MG_DB_Attachments.I.offsetX;
			attach_offsetY = MG_DB_Attachments.I.offsetY;

			attachment_obj.transform.SetParent(MG_GetUnit.I._getUnitWithID(ownerID).sprite.transform);
			Vector3 pos = MG_GetUnit.I._getUnitWithID (ownerID).sprite.transform.position;
			attachment_obj.transform.position = new Vector3 (pos.x + attach_offsetX, pos.y + attach_offsetY, pos.z-0.5f);
		}
	}

	public void _updateAttachPosition(MG_ClassUnit ownerUnit){
		attachment_has = MG_DB_Attachments.I.hasAttachment(type);
		if (attachment_has && attachment_obj != null) {
			MG_DB_Attachments.I._setAttachmentData (type);
			attach_offsetX = MG_DB_Attachments.I.offsetX;
			attach_offsetY = MG_DB_Attachments.I.offsetY;

			attachment_obj.transform.SetParent (ownerUnit.sprite.transform);
			Vector3 pos = MG_GetUnit.I._getUnitWithID (ownerID).sprite.transform.position;
			attachment_obj.transform.position = new Vector3 (pos.x + attach_offsetX, pos.y + attach_offsetY, pos.z-2.5f);
		}
	}

	public void destroy(){
		if (attachment_has) {
			MG_ControlUnits.I._destroyUIObject (attachment_obj);
		}
	}
}
