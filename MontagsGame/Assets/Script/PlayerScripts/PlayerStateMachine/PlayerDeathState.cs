using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public Shader deathShader;
    PlayerController playerController;
    private Animator animator;
    private AnimatorClipInfo[] clipInfo;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Stato Player = Morto");
        playerController = player.GetComponent<PlayerController>();
        animator = playerController.animator;

        //Viene nascosta la pistola
        playerController.weapon.SetActive(false);
  		animator.SetBool("isDeath", true);
  		//Per disabilitare il RigBuilder
  		playerController.rigBuilder.enabled = false;

        //Per gestire il passaggio allo shader per la dissolvenza
        //L'intensita dello shader per la dissolvenza viene settato inizialmente a 0.3
        playerController.material[0].SetFloat("Vector_Intensity_Dissolve2", 0.3f);
        //I materiali del personaggio vengono settati al materiale con lo shader per la dissolvenza
        playerController.renderAstroBody.sharedMaterials = playerController.material;
    	playerController.renderAstroHead.sharedMaterials = playerController.material;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (string.Equals(GetCurrentClipName(), "MortePersonaggio") && playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            //quando finisce l'animazione scompare il personaggio (da cambiare)
            Object.Destroy(player.gameObject);
        }

        //Per gestire la dissolvenza durante la morte del MortePersonaggio
        if (string.Equals(GetCurrentClipName(), "MortePersonaggio"))
        {
            //Man mano che l'animazione va avanti l'intensita dello shader della dissolvenza aumenta di valore
            playerController.material[0].SetFloat("Vector_Intensity_Dissolve2", playerController.material[0].GetFloat("Vector_Intensity_Dissolve2") + 0.005f);
        }
    }

    //Funzione necessaria per risalire al nome dell'animazione corrente
    public string GetCurrentClipName()
    {
        clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo[0].clip.name;
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision)
    {

    }
}
